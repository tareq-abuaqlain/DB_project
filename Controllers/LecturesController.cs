using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDB.Models;
using ProjectDB.Services;

namespace ProjectDB.Controllers
{
    public class LecturesController : Controller
    {
        private readonly IManageLecturesService service;
        private readonly IManageDivService divService;

        public LecturesController(IManageLecturesService service,
            IManageDivService divService)
        {
            this.service = service;
            this.divService = divService;
        }


        // GET: LecturesController
        public ActionResult Index(int div_id)
        {
            return View(service.GetAllLecturesByDiv(div_id));
        }

        // GET: LecturesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LecturesController/Create
        public ActionResult Create(int div_id)
        {
            var lec = new Lecture();
            lec.div = divService.GetDivById(div_id);
            return View(lec);
        }

        // POST: LecturesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int div_id , Lecture lecture)
        {
            try
            {
                if(lecture != null)
                {
                    lecture.div = divService.GetDivById (div_id);
                    lecture.time = DateTime.Now;
                    if (service.AddLecture(lecture))
                    {
                        return RedirectToAction(nameof(Index),new {div_id = div_id});
                    }
                }
                return RedirectToAction(nameof(Create), new { div_id = div_id });
            }
            catch
            {
                return RedirectToAction(nameof(Create) , new {div_id = div_id});
            }
        }

        // GET: LecturesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LecturesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LecturesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LecturesController/Delete/5
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
