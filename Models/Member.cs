using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcShopping.Models
{
    [DisplayName("会员信息")]
    [DisplayColumn("Name")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("会员账号")]
        [Required(ErrorMessage = "请输入 Email 地址")]
        [Description("我们直接用 Email 来当做会员的登录账号")]
        [MaxLength(250, ErrorMessage = "请输入正确的Email")]
        [Remote("CheckDup", "Member", ErrorMessage = "您输入的邮箱有人注册过了")]
        [DataType(DataType.EmailAddress,ErrorMessage = "请输入正确的Email")]
        public string Email { get; set; }

        [DisplayName("会员密码")]
        [Required(ErrorMessage = "请输入密码")]
        [MaxLength(40, ErrorMessage = "密码长度不能超过40个字符")]
        [Description("密码用Hash加密,加密后的密码格式皆为40个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("中文名字")]
        [Required(ErrorMessage = "请输入中文名称")]
        [MaxLength(5, ErrorMessage ="不能超过5个字符")]
        [Description("暂不考虑有外国人注册的情况")]
        public string Name { get; set; }

        [DisplayName("网络昵称")]
        [Required(ErrorMessage ="请输入网络昵称")]
        [MaxLength(10,ErrorMessage ="最大长度为10个字符")]
        public string Nickname { get; set; }

        [DisplayName("会员注册时间")]
        public DateTime RegisterOn { get; set; }

        [DisplayName("会员启用验证码")]
        [MaxLength(36)]
        [Description("当此字段只为空时表示已经通过验证")]
        public string AuthCode { get; set; }

        public virtual ICollection<OrderHeader> Orders { get; set; }
    }
}