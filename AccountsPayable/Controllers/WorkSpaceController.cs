using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountsPayable.Models;

namespace AccountsPayable.Controllers
{
    public class WorkSpaceController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();

        // GET: WorkSpace
        public ActionResult Index()
        {
            var tB_WORKSPACE = db.TB_WORKSPACE.Include(t => t.TB_APPROVER).Include(t => t.TB_HIGHLIGHTS).Include(t => t.TB_HISTORIC_REMIT).Include(t => t.TB_ORACLE_LEGAL_ENTITIES).Include(t => t.TB_ORACLE_PAY_TERMS).Include(t => t.TB_ORACLE_SOURCE).Include(t => t.TB_ORACLE_TYPE);
            return View(tB_WORKSPACE.ToList());
        }

        // GET: WorkSpace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.Find(id);
            if (tB_WORKSPACE == null)
            {
                return HttpNotFound();
            }
            return View(tB_WORKSPACE);
        }

        // GET: WorkSpace/Create
        public ActionResult Create()
        {
            ViewBag.WS_FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME");
            ViewBag.WS_FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGHLIGHTS_ID", "HIGHLIGHTS");
            ViewBag.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(db.TB_HISTORIC_REMIT, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_INFO");
            ViewBag.WS_FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
            ViewBag.WS_FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
            ViewBag.WS_FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
            ViewBag.WS_FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
            return View();
        }

        // POST: WorkSpace/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WS_ID,TEMP_ID,FK_WS_INVOICE_CATEGORY_ID,WS_STATUS,WS_REASON,WS_EMAIL_RECEIVED,WS_CREATED_DATE,WS_SOURCE,WS_HANDLED_BY,WS_INVOICE_DATE,WS_AMOUNT,WS_INVOICE_NUMBER,FK_WS_COMMENTS_ID,FK_WS_LAST_ACTIONS_ID,WS_ISDISABLED,WS_TEMP_TAX_ID,WS_TEMP_REMIT_TO,WS_TEMP_SUPPLIER_NAME,WS_TEMP_SUPPLIER_NUMBER,WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID,WS_TEMP_VENDOR_ACCOUNT,WS_FK_TB_TEMPLATE_ALIAS_ID,WS_TEMP_SUPPLIER_SITE,WS_TEMP_ADDRESS,WS_FK_TB_LEGAL_ENTITY_ID,WS_TEMP_TAXPAYER_ID,WS_FK_TB_ORACLE_TYPE_ID,WS_TEMP_ORACLE_DESCRIPTION,WS_FK_TB_ORACLE_PAY_TERMS_ID,WS_TEMP_ACCOUNT_CODING,WS_FK_TB_ORACLE_SOURCE_ID,WS_TEMP_ORACLE_NOTES,WS_TEMP_ORACLE_INSTRUCTIONS,WS_FK_TB_HIGHLIGHTS_ID,WS_FK_TB_EMAIL_BACKUP_ID,WS_FK_TB_APPROVER_ID,WS_TEMP_APPROVER_COMMENTS,WS_TEMP_INVOICE_FORMAT,WS_TEMP_INVOICE_TYPE,WS_TEMP_ISDISABLED,WS_TEMP_FOLDER,WS_TEMP_PAYMENT_METHOD,WS_TEMP_REMIT_TOACCOUNT,WS_TEMP_BILLING_PERIOD,WS_TEMP_DISTRIBUTION_COMBINATION,WS_TEMP_ACCOUNTING_DATE,WS_TEMP_VSU,WS_TEMP_W9_W8,WS_TEMP_INVOICE_NOTES,WS_TEMP_INVOICE_DESCRIPTION,WS_TEMP_BILLING_PERIOD_DATE,WS_CONTACTS_CURRENT,WS_FK_TB_ORGANIZATION_TYPE_ID")] TB_WORKSPACE tB_WORKSPACE)
        {
            if (ModelState.IsValid)
            {
                db.TB_WORKSPACE.Add(tB_WORKSPACE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WS_FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_WORKSPACE.WS_FK_TB_APPROVER_ID);
            ViewBag.WS_FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGHLIGHTS_ID", "HIGHLIGHTS", tB_WORKSPACE.WS_FK_TB_HIGHLIGHTS_ID);
            ViewBag.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(db.TB_HISTORIC_REMIT, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_INFO", tB_WORKSPACE.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID);
            ViewBag.WS_FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_WORKSPACE.WS_FK_TB_LEGAL_ENTITY_ID);
            ViewBag.WS_FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_PAY_TERMS_ID);
            ViewBag.WS_FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_SOURCE_ID);
            ViewBag.WS_FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_WORKSPACE.WS_FK_TB_ORACLE_TYPE_ID);
            return View(tB_WORKSPACE);
        }

        // GET: WorkSpace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.Find(id);
            if (tB_WORKSPACE == null)
            {
                return HttpNotFound();
            }
            ViewBag.WS_FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_WORKSPACE.WS_FK_TB_APPROVER_ID);
            ViewBag.WS_FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGHLIGHTS_ID", "HIGHLIGHTS", tB_WORKSPACE.WS_FK_TB_HIGHLIGHTS_ID);
            ViewBag.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(db.TB_HISTORIC_REMIT, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_INFO", tB_WORKSPACE.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID);
            ViewBag.WS_FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_WORKSPACE.WS_FK_TB_LEGAL_ENTITY_ID);
            ViewBag.WS_FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_PAY_TERMS_ID);
            ViewBag.WS_FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_SOURCE_ID);
            ViewBag.WS_FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_WORKSPACE.WS_FK_TB_ORACLE_TYPE_ID);
            return View(tB_WORKSPACE);
        }

        // POST: WorkSpace/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WS_ID,TEMP_ID,FK_WS_INVOICE_CATEGORY_ID,WS_STATUS,WS_REASON,WS_EMAIL_RECEIVED,WS_CREATED_DATE,WS_SOURCE,WS_HANDLED_BY,WS_INVOICE_DATE,WS_AMOUNT,WS_INVOICE_NUMBER,FK_WS_COMMENTS_ID,FK_WS_LAST_ACTIONS_ID,WS_ISDISABLED,WS_TEMP_TAX_ID,WS_TEMP_REMIT_TO,WS_TEMP_SUPPLIER_NAME,WS_TEMP_SUPPLIER_NUMBER,WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID,WS_TEMP_VENDOR_ACCOUNT,WS_FK_TB_TEMPLATE_ALIAS_ID,WS_TEMP_SUPPLIER_SITE,WS_TEMP_ADDRESS,WS_FK_TB_LEGAL_ENTITY_ID,WS_TEMP_TAXPAYER_ID,WS_FK_TB_ORACLE_TYPE_ID,WS_TEMP_ORACLE_DESCRIPTION,WS_FK_TB_ORACLE_PAY_TERMS_ID,WS_TEMP_ACCOUNT_CODING,WS_FK_TB_ORACLE_SOURCE_ID,WS_TEMP_ORACLE_NOTES,WS_TEMP_ORACLE_INSTRUCTIONS,WS_FK_TB_HIGHLIGHTS_ID,WS_FK_TB_EMAIL_BACKUP_ID,WS_FK_TB_APPROVER_ID,WS_TEMP_APPROVER_COMMENTS,WS_TEMP_INVOICE_FORMAT,WS_TEMP_INVOICE_TYPE,WS_TEMP_ISDISABLED,WS_TEMP_FOLDER,WS_TEMP_PAYMENT_METHOD,WS_TEMP_REMIT_TOACCOUNT,WS_TEMP_BILLING_PERIOD,WS_TEMP_DISTRIBUTION_COMBINATION,WS_TEMP_ACCOUNTING_DATE,WS_TEMP_VSU,WS_TEMP_W9_W8,WS_TEMP_INVOICE_NOTES,WS_TEMP_INVOICE_DESCRIPTION,WS_TEMP_BILLING_PERIOD_DATE,WS_CONTACTS_CURRENT,WS_FK_TB_ORGANIZATION_TYPE_ID")] TB_WORKSPACE tB_WORKSPACE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_WORKSPACE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WS_FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_WORKSPACE.WS_FK_TB_APPROVER_ID);
            ViewBag.WS_FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGHLIGHTS_ID", "HIGHLIGHTS", tB_WORKSPACE.WS_FK_TB_HIGHLIGHTS_ID);
            ViewBag.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(db.TB_HISTORIC_REMIT, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_INFO", tB_WORKSPACE.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID);
            ViewBag.WS_FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_WORKSPACE.WS_FK_TB_LEGAL_ENTITY_ID);
            ViewBag.WS_FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_PAY_TERMS_ID);
            ViewBag.WS_FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_SOURCE_ID);
            ViewBag.WS_FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_WORKSPACE.WS_FK_TB_ORACLE_TYPE_ID);
            return View(tB_WORKSPACE);
        }

        // GET: WorkSpace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.Find(id);
            if (tB_WORKSPACE == null)
            {
                return HttpNotFound();
            }
            return View(tB_WORKSPACE);
        }

        // POST: WorkSpace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.Find(id);
            db.TB_WORKSPACE.Remove(tB_WORKSPACE);
            db.SaveChanges();
            return RedirectToAction("Index");
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
