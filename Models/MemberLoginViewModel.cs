using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcShopping.Models
{
    public class MemberLoginViewModel
    {
        [DisplayName("会员账号")]
        [DataType(DataType.EmailAddress,ErrorMessage ="请输入正确的邮箱地址")]
        [Required(ErrorMessage ="请输入{0}")]
        public string email { get; set; }

        [DisplayName("会员密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="请输入{0}")]
        public string password { get; set; }
    }
}