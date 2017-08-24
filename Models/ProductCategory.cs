using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace MvcShopping.Models
{
    [DisplayName("商品类别")]
    [DisplayColumn("Name")]
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("商品类别名称")]
        [Required(ErrorMessage ="请输入商品类别")]
        [MaxLength(20,ErrorMessage ="最大20个字")]
        public string Name { get; set; }


        [DisplayName("类别图片")]
        [Required(ErrorMessage = "暂无图片")]
        public string CategoryImg { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}