using System.Collections.Generic;

namespace qlcdvien.Controllers
{
    public class TreeNode
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public TreeNode ParentCategory { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}