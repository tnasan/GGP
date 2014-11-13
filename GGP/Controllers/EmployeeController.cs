﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGP.Models;

namespace GGP.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                return View(db.Employees.ToList());
            }
        }

        public ActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            using (GGPDBEntities db = new GGPDBEntities())
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                return View(employee);
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
                return View(db.Employees.Find(id));
            }
        }

        public ActionResult Edit(Employee employee)
        {
            if (employee == null || employee.Id <= 0)
            {
                return RedirectToAction("Index");
            }

            using (GGPDBEntities db = new GGPDBEntities())
            {
                Employee dbEmployee = db.Employees.Find(employee.Id);
                if (dbEmployee == null)
                {
                    return RedirectToAction("Index");
                }

                dbEmployee.Firstname = employee.Firstname;
                dbEmployee.Lastname = employee.Lastname;
                dbEmployee.Alias = employee.Alias;
                dbEmployee.NationalIdentificationNumber = employee.NationalIdentificationNumber;
                dbEmployee.TelephoneNumber = employee.TelephoneNumber;
                dbEmployee.Email = employee.Email;
                dbEmployee.BirthDate = employee.BirthDate;
                dbEmployee.StartWorkingDate = employee.StartWorkingDate;
                dbEmployee.WorkingStatusId = employee.WorkingStatusId;
                dbEmployee.NationalityId = employee.NationalityId;
                if (employee.AdditionalDocument != null)
                {
                    if (dbEmployee.AdditionalDocument == null)
                    {
                        dbEmployee.AdditionalDocument = new AdditionalDocument();
                    }
                    dbEmployee.AdditionalDocument.TemporaryLivingAllowance = employee.AdditionalDocument.TemporaryLivingAllowance;
                    dbEmployee.AdditionalDocument.Passport = employee.AdditionalDocument.Passport;
                    dbEmployee.AdditionalDocument.PassportNumber = employee.AdditionalDocument.PassportNumber;
                    dbEmployee.AdditionalDocument.PassportIssueDate = employee.AdditionalDocument.PassportIssueDate;
                    dbEmployee.AdditionalDocument.PassportExpiredDate = employee.AdditionalDocument.PassportExpiredDate;
                    dbEmployee.AdditionalDocument.VISA = employee.AdditionalDocument.VISA;
                    dbEmployee.AdditionalDocument.VISANumber = employee.AdditionalDocument.VISANumber;
                    dbEmployee.AdditionalDocument.VISAExpiredDate = employee.AdditionalDocument.VISAExpiredDate;
                    dbEmployee.AdditionalDocument.NationalityVerification = employee.AdditionalDocument.NationalityVerification;
                    dbEmployee.AdditionalDocument.WorkPermit = employee.AdditionalDocument.WorkPermit;
                    dbEmployee.AdditionalDocument.WorkPermitNumber = employee.AdditionalDocument.WorkPermitNumber;
                    dbEmployee.AdditionalDocument.WorkPermitExpiredDate = employee.AdditionalDocument.WorkPermitExpiredDate;
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}