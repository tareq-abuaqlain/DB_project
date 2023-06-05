using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDB.Models;
using ProjectDB.Services;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;

namespace ProjectDB.Controllers
{
    public class AttendController : Controller
    {
        private readonly IManageAttendService service;
        private readonly IManageLecturesService lecturesService;
        private readonly IManageStudentsService studentsService;
        private readonly IManageRegestrationService regestrationService;

        public AttendController(IManageAttendService service,
            IManageLecturesService lecturesService,
            IManageStudentsService studentsService,
            IManageRegestrationService regestrationService)
        {
            this.service = service;
            this.lecturesService = lecturesService;
            this.studentsService = studentsService;
            this.regestrationService = regestrationService;
        }



        // GET: AttendController
        public ActionResult Index(int lecture_id , List<string?>? inValid = null)
        {
            var lec = lecturesService.GetLecture(lecture_id);
            List<Student>? students = null;
            if (lec != null)
                students = regestrationService.GetAllStudentsInDiv(lec.div!.div_id);
            else
                return RedirectToAction("IndexByUser", "Divs", new { username = HttpContext.Session.GetString("username") });
            if (students == null || students.Count() == 0)
            {
                return RedirectToAction("IndexByUser", "Divs", new { username = HttpContext.Session.GetString("username") });
            }
            var attends = new List<Attend>();
            foreach (var student in students)
            {
                Attend? attend = service.GetAttend(student.std_id , lec.lecture_id);
                if(attend != null)
                    attends.Add(attend);
            }
            if (inValid == null)
                ViewData["inValid"] = new List<string>();
            else
                ViewData["inValid"] = inValid;
            ViewData["students"] = students;
            ViewData["lec_id"] = lecture_id;
            ListAttend listAttend = new ListAttend();
            listAttend.attends = attends;
            return View(listAttend);
        }

        // GET: AttendController/Details/5
        public ActionResult AddExcel(IFormFile file , int lecture_id)
        {
            if (file != null && file.Length > 0)
            {
                List<string?> inValid = new List<string?>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()) )
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming the first worksheet
                    var lec = lecturesService.GetLecture(lecture_id);
                    if (lec != null)
                    {
                        var students = regestrationService.GetAllStudentsInDiv(lec.div!.div_id);
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;
                        if (colCount == 1)
                        {
                            for (int row = 1; row <= rowCount; row++)
                            {
                                var attend = new Attend();
                                string? std_string = worksheet.Cells[row, 1].Value?.ToString();
                                int std_id = 0;
                                if (int.TryParse(std_string, out std_id))
                                {
                                    attend.student = studentsService.GetStudent(std_id);
                                    if (attend.student != null)
                                    {
                                        attend.lecture = lecturesService.GetLecture(lecture_id);
                                        if (attend.lecture != null)
                                        {
                                            attend.isAttend = true;
                                            if (!service.AddAttend(attend))
                                            {
                                                inValid.Add(std_string);
                                            }
                                        }
                                        else
                                        {
                                            inValid.Add(std_string);
                                        }
                                    }
                                    else
                                    {
                                        inValid.Add(std_string);
                                    }

                                }
                                else
                                {
                                    inValid.Add(std_string);
                                }
                                
                                
                            }
                            if (students != null)
                            {
                                foreach (var student in students)
                                {
                                    var attend = service.GetAttend(student.std_id, lecture_id);
                                    if (attend == null)
                                    {
                                        attend = new Attend();
                                        attend.student = student;
                                        attend.lecture = lecturesService.GetLecture(lecture_id);
                                        attend.isAttend = false;
                                        service.AddAttend(attend);
                                    }
                                }
                            }
                            // Process the cell value as needed
                            return RedirectToAction(nameof(Index), new { lecture_id = lecture_id, inValid = inValid });
                        }

                    }
                }
            }
            return RedirectToAction(nameof(Index), new { lecture_id = lecture_id });
        }

        // GET: AttendController/Create
        //public ActionResult Create(int lecture_id)
        //{
        //    var lec = lecturesService.GetLecture(lecture_id);
        //    List<Student>? students = null;
        //    if (lec != null)
        //        students = regestrationService.GetAllStudentsInDiv(lec.div!.div_id);
        //    else
        //        return RedirectToAction("IndexByUser", "Divs", new { username = HttpContext.Session.GetString("username") });
        //    if (students ==null || students.Count() == 0)
        //    {
        //        return RedirectToAction("IndexByUser", "Divs" , new {username = HttpContext.Session.GetString("username")});
        //    }
        //    var attends = new List<Attend>();
        //    foreach (var student in students)
        //    {
        //        var attend = new Attend();
        //        attend.student = student;
        //        attend.lecture = lec;
        //        attend.isAttend = false;
        //        attends.Add(attend);
        //    }
        //    ListAttend listAttend = new ListAttend();
        //    listAttend.attends = attends;
        //    return View(listAttend);
        //}

        // POST: AttendController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(int std_id , int lecture_id , bool isAttend)
        {
            try
            {
                var attend = service.GetAttend(std_id, lecture_id);
                if (attend == null) 
                {
                    attend = new Attend();
                    attend.student = studentsService.GetStudent(std_id);
                    attend.lecture = lecturesService.GetLecture(lecture_id);
                    attend.isAttend = isAttend;
                    service.AddAttend(attend);
                }
                else
                {
                    return RedirectToAction(nameof(Edit),new {std_id = std_id , lecture_id = lecture_id , isAttend = isAttend});
                }
                return RedirectToAction(nameof(Index),new {lecture_id = lecture_id });
            }
            catch
            {
                return RedirectToAction(nameof(Index), new { lecture_id = lecture_id });
            }
        }

        // GET: AttendController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AttendController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int std_id, int lecture_id, bool isAttend)
        {
            try
            {
                var attend = service.GetAttend(std_id, lecture_id);
                if (attend != null)
                {
                    attend.isAttend=isAttend;
                    service.UpdateAttend(attend);
                }
                return RedirectToAction(nameof(Index), new { lecture_id = attend!.lecture!.lecture_id });
            }
            catch
            {
                return View();
            }
        }

        // GET: AttendController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttendController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
