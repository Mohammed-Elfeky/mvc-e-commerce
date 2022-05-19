using Microsoft.AspNetCore.Mvc;
using System.Linq;
using e_commerce_project.Models;
using e_commerce_project.Models.postRequestsModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace e_commerce_project.Controllers
{
    public class CartController : Controller
    {
        context db;
        public CartController(context db)
        {
            this.db = db;
        }
        [Authorize]
        public IActionResult Add(int id)
        {
            //get the loggedin user id
            string userId = User.
                         Claims.
                         FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                         .Value;

            var userHasUnSuubmittedOrdr = db.
                                          Orders.
                                          Where(o => o.user_id == userId && o.submitted == false).
                                          SingleOrDefault()
                                         ;

            #region check if there is UnSubmitted order for this user
            if (userHasUnSuubmittedOrdr != null)
            {
                #region check if the product  exists in the order

                OrderProduct productExist = db.OrdersProducts.Where(
                    op => op.OrderId == userHasUnSuubmittedOrdr.Id && op.ProductId == id
                ).SingleOrDefault();
                if (productExist != null)
                {
                    //increase the quantity of the product
                    productExist.quantity += 1;
                    db.SaveChanges();
                    //go to 
                    return RedirectToAction("getOrderCount", new { id = userHasUnSuubmittedOrdr.Id });
                }
                #endregion


                #region if the product doesnt exist in the order
                //if the product doesnt exists in the order
                //create new orderproduct row
                //assign the product id to it and make the quantity 1
                OrderProduct opRow = new OrderProduct()
                {
                    OrderId = userHasUnSuubmittedOrdr.Id,
                    ProductId = id,
                    quantity = 1
                };
                db.OrdersProducts.Add(opRow);
                db.SaveChanges();
                return RedirectToAction("getOrderCount", new { id = userHasUnSuubmittedOrdr.Id }); 
                #endregion
            }
            #endregion

            #region if there is no unsubmitted order assigned to that user
            //create unsubmitted order 
            //and assign user id to the order
            Order newOrder = new Order() { user_id = userId, submitted = false,deliverd=false };
            db.Orders.Add(newOrder);
            db.SaveChanges();
            int createdOrderId = newOrder.Id;

            //assign the created order to the product in the orderProduct Table
            OrderProduct ob = new OrderProduct()
            {
                OrderId = createdOrderId,
                ProductId = id,
                quantity = 1
            };
            db.OrdersProducts.Add(ob);
            db.SaveChanges();
            return Json(new {count=1});
            #endregion

        }
        public IActionResult GetUserOrder()
        {
            //get the loggedin user id
            string userId =User.
                        Claims.
                        FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                        .Value;

            //check if there is UnSubmitted order for this user
            Order theUnSubmittedOrder = db.Orders.
                            Where(o => o.user_id == userId && o.submitted == false)
                            .SingleOrDefault()
                                          ;
            //if there is display it
            if (theUnSubmittedOrder != null)
            {
                var order = db.OrdersProducts.
                            Include(op => op.Product).
                            Where(op => op.OrderId == theUnSubmittedOrder.Id).
                            ToList();
                return Json(new { orderExist = true, order = order});
            }
            return Json(new { orderExist = false,msg="no orders to show"});
        }
        [Authorize]
        public IActionResult ShowOrder()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Submit(int id, [FromBody] Address address)
        {
            #region change the submitted col of the order to true
                Order o = db.Orders.Find(id);
                decimal total = db.OrdersProducts.Include(op=>op.Product)
                            .Where(op=>op.OrderId==id)
                            .Sum(op=>(op.quantity*op.Product.Price));
                o.submitted = true;
                o.TotalPrice = total;
                o.date = DateTime.Now;
                o.address = address.name;
                o.aLat = address.lat;
                o.aLong = address.longg;
                db.SaveChanges();
            #endregion
            return Json(new {});
        }
        public IActionResult Clear(int id)
        {
            #region remove the order rows from orderProduct table
                List <OrderProduct> orderProductsToDelete = db.OrdersProducts.
                                                            Where(op=>op.OrderId==id).ToList();
                Order o=db.Orders.Where(order=>order.Id==id).Single();
                foreach(OrderProduct op in orderProductsToDelete)
                {
                    db.OrdersProducts.Remove(op);
                }
                db.Orders.Remove(o);
                db.SaveChanges();
            return Json(new {cleared=true});
            #endregion
        }
        public IActionResult remove(int id,int productId)
        {
            #region if this is the last product in the order and the quantity 1
                List<OrderProduct> order=db.OrdersProducts.
                                   Where(op=>op.OrderId==id).ToList();

            //only one product in the cart
            if (order.Count() == 1)
            {
                if (order[0].quantity == 1)
                {
                    return RedirectToAction("Clear",new {id=id});
                }
                else
                {
                    order[0].quantity -= 1;
                    db.SaveChanges();
                    return Json(new { count=order.Count()});
                }
            }
            
            OrderProduct theProduct = db.OrdersProducts.
                                                Where(op => op.ProductId == productId && op.OrderId == id)
                                                .SingleOrDefault();
            if (theProduct.quantity == 1)
            {
                db.OrdersProducts.Remove(theProduct);
            }
            else
            {
                theProduct.quantity -= 1;
            }
            db.SaveChanges();
            return RedirectToAction("getOrderCount",new {id=theProduct.OrderId});
            #endregion
        }

        public IActionResult getOrderCount(int id)
        {
            return Json(new { count = db.OrdersProducts.Where(op => op.OrderId == id).Count() });
                
        }

        public IActionResult getOrderCountWhenNotId()
        {
            //get the loggedin user id
            if(User.Claims.Count() != 0)
            {
                string userId = User.
                            Claims.
                            FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                            .Value;
                Order theUnSubmittedOrder = db.Orders.
                            Where(o => o.user_id == userId && o.submitted == false)
                            .SingleOrDefault();

               if(theUnSubmittedOrder != null)
                {
                    var count=db.OrdersProducts.Where(op=>op.OrderId == theUnSubmittedOrder.Id).Count();
                    return Json(new { count });
                }
            }
            return Json(new { });
        }
    }
}
