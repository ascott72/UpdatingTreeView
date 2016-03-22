using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UpdatingTreeView.Models;


namespace UpdatingTreeView.Controllers
{
    public class TreeViewModelsController : Controller
    {
        private UpdatingTreeViewContext db = new UpdatingTreeViewContext();

        // GET: TreeViewModels
        public ActionResult Index()
        {
            return View(db.TreeViewModels.ToList());
        }


        public JsonResult GetTreeData(int upperID)
        {
            var dataList = db.TreeViewModels.Where(a => upperID.Equals(a.ParentID));
            return Json(dataList);
        }

        public ActionResult Create()
        {
            return PartialView("Create");
        }
        // POST: TreeViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NodeName")] TreeViewModel treeViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                db.TreeViewModels.Add(treeViewModel);
                db.SaveChanges();
                message = "Success";          
            }
            else
            {
                message = "Create Unsuccessful";
            }

            return new JsonResult { Data = new { message = message } };
        }

        // GET: TreeViewModels/AddNodes/5
        public ActionResult AddNodes(int id)
        {
            if (id <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeViewModel tvm = db.TreeViewModels.Find(id);
            if (tvm == null)

            {
                return HttpNotFound();
            }
            return PartialView("AddNodes",tvm);
        }

        // POST: TreeViewModels/AddNodes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNodes([Bind(Include = "NodeName, NodeID")] TreeViewModel tvm, int rangeInput, int lowBound, int upperBound)
        {
            Random rnd = new Random();
            string message = "";
            string rn = "";
            var previous = db.TreeViewModels.Where(a => a.ParentID == tvm.NodeID).ToList();
            String messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v =>
             v.Errors).Select(v => v.ErrorMessage + " " + v.Exception));
            if (ModelState.IsValid)
            {
                foreach (var p in previous)
                    db.TreeViewModels.Remove(p);
                db.SaveChanges();

                for (int i = 0; i < rangeInput; i++)
                {

                    rn = rnd.Next(lowBound, upperBound).ToString();

                    db.TreeViewModels.Add(new TreeViewModel { NodeName = rn, ParentID = tvm.NodeID });
                }
                db.SaveChanges();
                message ="Successfully Added Nodes";
            }
            else
            {

                message = messages;
            }

            return new JsonResult { Data = new { message = message } };
        }

        // GET: TreeViewModels/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeViewModel tvm = db.TreeViewModels.Find(id);
            Console.WriteLine("tvm", tvm);
            if (tvm == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", tvm);
        }

        // POST: TreeViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NodeName,NodeID,ParentID")] TreeViewModel treeViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                db.Entry(treeViewModel).State = EntityState.Modified;
                db.SaveChanges();
                message = "Edit Successful";
            }
            else
            {
                message = "Error! Edit Unsuccessful";
            }
            return new JsonResult { Data = new { message = message } };
        }

        //// GET: TreeViewModels/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id==null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TreeViewModel tvm = db.TreeViewModels.Find(id);
        //    if (tvm == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("Delete",tvm);
        //}

        // POST: TreeViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            int deleteID = Convert.ToInt32(id);
            string message = "";
            TreeViewModel treeViewModel = db.TreeViewModels.Find(deleteID);
            if (treeViewModel != null)
            {
                db.TreeViewModels.Remove(treeViewModel);
                db.SaveChanges();
                message = "Successfully Deleted.";
            }
            else
            {
                message = "Delete Unsucessful";
            }
            return new JsonResult { Data = new { message = message } };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
