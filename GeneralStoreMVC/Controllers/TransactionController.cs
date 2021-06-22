using GeneralStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {

        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(tran => tran.DateOfTransaction).ToList();
            return View(orderedList);
        }

        // GET: Transaction
        public ActionResult Create()
        {
            // Research more into 'VIEWBAG'
            ViewBag.CustomerId = new SelectList(_db.Customers.ToList(), "CustomerId", "FullName");
            ViewBag.ProductId = new SelectList(_db.Products.ToList(), "ProductId", "Name");
            return View();
        }

        // POST: Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                Product product = _db.Products.Find(transaction.ProductId);
                if (product == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = _db.Customers.Find(transaction.CustomerId);
                if (customer == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (transaction.PurchaseQuantity > product.InventoryCount)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                product.InventoryCount -= transaction.PurchaseQuantity;

                transaction.DateOfTransaction = DateTime.Now;
                _db.Transactions.Add(transaction);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Research more into 'VIEWBAG'

            ViewBag.CustomerId = new SelectList(_db.Customers.ToList(), "CustomerId", "FullName");
            ViewBag.ProductId = new SelectList(_db.Products.ToList(), "ProductId", "Name");
            return View(transaction);
        }

        // GET: Delete
        // Transaction/Delete/{id}
        // Do I need to delete a transaction??? NO!

        // GET: Details
        // Transaction/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _db.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
    }
}