using AccountsPayable.Models;
using System.Linq;
using System.Web.Mvc;

namespace AccountsPayable.Controllers
{
    public class HistoricController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();
        [HttpGet]
        public ActionResult GetEmailText(int emailId, int id)
        {
            // Get the text based on the ID
            var historicEmailText = db.TB_EMAIL_BACKUP
                    .Where(a => a.EMAIL_BACKUP_ID == emailId)
                    .Select(item => item.EMAIL_BACKUP)
                    .FirstOrDefault();            
            //var historicEmailText = db.TB_EMAIL_BACKUP
            //        .Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id) && a.EMAIL_BACKUP_ID == emailId)
            //        .Select(item => item.EMAIL_BACKUP)
            //        .FirstOrDefault();
            if (historicEmailText != null)
            {
                return Json(new { success = true, historicEmailText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult GetHistoricRemitText(int historicId, int id)
        {
            // Get the text based on the ID
            var historicText = db.TB_HISTORIC_REMIT
                    .Where(a => a.HISTORIC_REMIT_ID == historicId)
                    .Select(item => item.HISTORIC_REMIT_INFO)
                    .FirstOrDefault();
            if (historicText != null)
            {
                return Json(new { success = true, historicText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult GetHighlights(int highlightsId, int id)
        {
            // Get the text based on the ID
            var historicText = db.TB_HIGHLIGHTS
                    .Where(a => a.HIGHLIGHTS_ID == highlightsId)
                    .Select(item => new
                    {
                        HIGHLIGHTS = item.HIGHLIGHTS,
                        HIGHLIGHTS_COMMENTS = item.HIGHLIGHTS_COMMENTS,
                        HIGHLIGHTS_INSTRUCTIONS = item.HIGHLIGHTS_INSTRUCTIONS,
                        HIGHLIGHTS_EXCEPTIONS = item.HIGHLIGHTS_EXCEPTIONS,
                        HIGHLIGHTS_COMMON_ISSUES = item.HIGHLIGHTS_COMMON_ISSUES,
                        HIGHLIGHTS_SUPPLIER_AGENCY = item.HIGHLIGHTS_SUPPLIER_AGENCY,
                        HIGHLIGHTS_TEMPLATE_COMMENTS = item.HIGHLIGHTS_TEMPLATE_COMMENTS,
                    })
                    .FirstOrDefault();
            if (historicText != null)
            {
                return Json(new { success = true, historicText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}