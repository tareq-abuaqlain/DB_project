using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjectDB.Models
{
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //[Required(ErrorMessage = "")]
        //[Display(Name = "username")]
        //[StringLength(9)]
        //[DataType(DataType.Text)]
        public string? username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //[Required(ErrorMessage = "كلمة المرور مطلوبة")]
        //[Display(Name = "كلمة المرور")]
        //[DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //[Display(Name = "تذكرني ؟ ")]
        public bool RememberMe { get; set; }
    }
}
