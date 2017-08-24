using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcShopping.Models
{
    [DisplayName("订单主文件")]
    [DisplayColumn("DisplayName")]
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("订购会员")]
        [Required]
        public virtual Member Member { get; set; }

        [DisplayName("收件人姓名")]
        [Required(ErrorMessage ="请输入收件人姓名")]
        [MaxLength(40,ErrorMessage ="收件人最大长度不能超过40个字符")]
        [Description("订购的会员不一定就是收到商品的人")]
        public string ContactName { get; set; }

        [DisplayName("联系电话")]
        [Required(ErrorMessage ="请输入您的电话")]
        [MaxLength(25,ErrorMessage ="电话长度最大25位")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhoneNo { get; set; }

        [DisplayName("送货地址")]
        [Required(ErrorMessage ="请输入收货地址")]
        public string ContactAddress { get; set; }  

        [DisplayName("订单金额")]
        [Required]
        [DataType(DataType.Currency)]
        [Description("由于订单金额可能会受商品传递方式或优惠折扣等方式异动价格,因此必须保留购买时算出来的订单金额")]
        public int TotalPrice { get; set; }

        [DisplayName("订单备注")]
        [DataType(DataType.MultilineText)]
        public string Memo { get; set; }

        [DisplayName("订购时间")]
        public DateTime ByOn { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get { return this.Member.Name + "于" + this.ByOn + "订购商品"; }
        }

        public virtual ICollection<OrderDetail> OrderDatailItems { get; set; }

    }
}