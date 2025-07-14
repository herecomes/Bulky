using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId, includeProperties: "Product"),
                OrderTotal = 0.0
            };

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnCount(cart);
                ShoppingCartVM.OrderTotal += cart.Price * cart.Count;
            }

            return View(ShoppingCartVM);
        }

        
        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cart.Count += 1;
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.Save();
            TempData["success"] = "Shopping cart updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if(cart.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                TempData["success"] = "Shopping cart updated successfully!";
            }
            else
            {
                cart.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cart);
                _unitOfWork.Save();
                TempData["success"] = "Shopping cart updated successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            TempData["success"] = "Shopping cart updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        private double GetPriceBasedOnCount(ShoppingCart cart)
        {
            if(cart.Count <= 50)
            {
                return cart.Product.Price;
            }
            else if(cart.Count <=100 && cart.Count > 50)
            {
                return cart.Product.Price50;
            }
            else
            {
                return cart.Product.Price100;
            }
        }
    }
}
