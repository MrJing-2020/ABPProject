using System.ComponentModel.DataAnnotations;

namespace ABPProject.CommonDto
{
    /// <summary>
    /// 参数为数组时（例：批量删除）
    /// </summary>
    public class ArrayParams
    {
        [Required]
        public int[] Ids { get; set; }
    }
}
