using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Inventories.Include("Customer").Include("Company").Include("UnifOfMeasurement").ToList());
            }
        }

        public ActionResult Create()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                ViewBag.Companies = new SelectList(db.Companies.ToList(), "Id", "Code");
                ViewBag.Customers = new SelectList(db.Customers.OrderBy(x => x.Name).ToList(), "Id", "Name");
                ViewBag.UMs = new SelectList(db.UnifOfMeasurements.OrderBy(x => x.Name).ToList(), "Id", "Name");

                return View(new Inventory());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory inventory)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                string codePrefix = String.Format("STCK{0:yyMMdd}-", DateTime.Today);
                string newCode = String.Empty;

                var findMaxCode = db.Inventories.Where(x => x.Code.StartsWith(codePrefix));
                newCode = findMaxCode.Any() ? String.Format("{0}{1:00}", codePrefix, Convert.ToInt32(findMaxCode.Max(x => x.Code.Replace(codePrefix, ""))) + 1) : String.Format("{0}01", codePrefix);

                inventory.Code = newCode;
                db.Inventories.Add(inventory);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                ViewBag.Companies = new SelectList(db.Companies.ToList(), "Id", "Code");
                ViewBag.Customers = new SelectList(db.Customers.OrderBy(x => x.Name).ToList(), "Id", "Name");
                ViewBag.UMs = new SelectList(db.UnifOfMeasurements.OrderBy(x => x.Name).ToList(), "Id", "Name");

                Inventory inventory = db.Inventories.Find(id);
                if (inventory == null)
                {
                    return RedirectToAction("Index");
                }

                return View(inventory);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inventory inventory)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                Inventory dbInventory = db.Inventories.Find(inventory.Id);
                if (inventory == null)
                {
                    return RedirectToAction("Index");
                }

                dbInventory.CompanyId = inventory.CompanyId;
                dbInventory.CustomerId = inventory.CustomerId;
                dbInventory.ItemName = inventory.ItemName;
                dbInventory.Quantity = inventory.Quantity;
                dbInventory.UnitOfMeasurementId = inventory.UnitOfMeasurementId;
                dbInventory.PricePerUnit = inventory.PricePerUnit;
                dbInventory.Place = inventory.Place;
                dbInventory.Remark = inventory.Remark;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Stat()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                List<Statistic> statistics = new List<Statistic>();
                statistics.Add(new Statistic
                    {
                        GroupName = "ภาพรวม",
                        ReportItems = new List<StatisticItem>()
                        {
                            new StatisticItem
                            {
                                Key = "มูลค่าทั้งหมด",
                                Value = db.Inventories.Sum(x => x.PricePerUnit * x.Quantity).ToString()
                            }
                        }
                    });
                statistics.Add(new Statistic
                    {
                        GroupName = "แยกตามลูกค้า",
                        ReportItems = db.Inventories.GroupBy(x => x.Customer).Select(x =>
                        new StatisticItem
                        {
                            Key = x.Key.Name,
                            Value = x.Sum(y => y.PricePerUnit * y.Quantity).ToString()
                        }).ToList()
                    });
                statistics.Add(new Statistic
                    {
                        GroupName = "แยกตามหน่วยสินค้า",
                        ReportItems = db.UnifOfMeasurements.Where(x => x.Inventories.Any()).Select(x =>
                        new StatisticItem
                        {
                            Key = x.Name,
                            Value = x.Inventories.Sum(y => y.PricePerUnit * y.Quantity).ToString()
                        }).ToList()
                    });
                return View(statistics);
            }
        }
    }
}