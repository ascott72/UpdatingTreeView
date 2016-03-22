using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UpdatingTreeView.Models
{
    public class TreeViewModel
    {
        [ScaffoldColumn(false)]
        [Key]
        public int NodeID { get; set; }
        
        [Required]
        public string NodeName { get; set; }

        [ScaffoldColumn(false)]
        public int ParentID { get; set; }

        [ScaffoldColumn(false)]
        public bool HasChild
        {
            get
            {
                UpdatingTreeViewContext db = new UpdatingTreeViewContext();
                var nodeList = db.TreeViewModels.ToList();

                return nodeList.Any(a => this.NodeID.Equals(a.ParentID));
            }
        }

        public TreeViewModel()
        {
            this.ParentID = 0;
        }


    }
}