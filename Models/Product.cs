using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MvcShopping.Models
{
    [DisplayName("商品信息")]
    [DisplayColumn("Name")]//对外显示的列
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("商品类别")]
        [Required]
        public virtual ProductCategory ProductCategory { get; set; }

        [DisplayName("商品名称")]
        [Required(ErrorMessage ="请输入商品名称")]
        [MaxLength(60,ErrorMessage ="商品名称不超过60个字")]
        public string Name { get; set; }

        [DisplayName("商品简介")]
        [Required(ErrorMessage ="请输入商品简介")]
        [MaxLength(250,ErrorMessage ="请不要输入超过250个字")]
        public string Description { get; set; }

        [DisplayName("商品图片")]
        [Required(ErrorMessage ="暂无图片")]
        public string ProductImgUrl { get; set; }

        [DisplayName("商品颜色")]
        [Required(ErrorMessage ="请选择商品颜色")]
        public Color Color { get; set; }

        [DisplayName("商品售价")]
        [Required(ErrorMessage ="请输入商品售价")]
        [Range(99,10000,ErrorMessage ="商品售价必须在99~10000之间")]
        public int Price { get; set; }

        [DisplayName("上架时间")]
        [Description("如果不设定上架时间,则代表此商品永远不上架")]
        public DateTime? PublishOn { get; set; }
    }
}