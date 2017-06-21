using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Iteration_1.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Iteration_1.Controllers
{
    public class BookingsController : Controller
    {
        private PnP_Mock_DBEntities1 db = new PnP_Mock_DBEntities1();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.MeetingRoom);
            return View(bookings.ToList());
        }

        public ActionResult Report()
        {
            return View();
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Seats");
            var dropdownDataList = db.MeetingRooms.ToList();

            var dropdownOptions = dropdownDataList.Select(d => new { d.RoomName, d.RoomId });
            ViewBag.DropdownListOptions = new SelectList(dropdownOptions, "RoomId", "RoomName");



            var employeeList = new MultiSelectList(new[] { "----Please Select Employees---", "Valerie", "Tshiani", "Lourena", "Banele", "Max", "Stephen" });
            ViewBag.EmployeeList = employeeList;



            var capacityList = new SelectList(new[] { "----Please Select Room Capacity---", "1", "2", "3", "4", "5", "6" });
            ViewBag.CapacityList = capacityList;


            var techList = new SelectList(new[] { "----Please Select Tech needed--", "Monitors", "Teleconferencing", "VideoConferencing", "Projector" });
            ViewBag.TechList = techList;


            return View();



        }




        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,RoomId,date,time")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                DateTime temp = booking.date;
                DateTime temp3 = booking.time;
                DateTime temp2 = temp.Date.Add(temp3.TimeOfDay);
                booking.BookedFor = temp2;
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index", "MeetingRooms");
            }

            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Seats", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Seats", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,RoomId,BookedFor")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.MeetingRooms, "RoomId", "Seats", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
