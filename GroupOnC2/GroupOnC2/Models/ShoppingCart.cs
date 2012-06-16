using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupOnC2.Models
{
	public class ShoppingCart
	{
		GROUPONEntities1 db = new GROUPONEntities1();
		
		string ShoppingCartId { get; set; } 

		public const string CartSessionKey = "CartId";

		public static ShoppingCart GetCart(HttpContextBase context)
		{
			var cart = new ShoppingCart();
			cart.ShoppingCartId = cart.GetCartId(context);
			return cart;
		}

		public static ShoppingCart GetCart(Controller controller)
		{
			return GetCart(controller.HttpContext);
		}

		public void AddToCart(SANPHAM sanPham)
		{
			var cartItem = db.CARTs1.SingleOrDefault(c => c.CartId == ShoppingCartId && c.MaSP == sanPham.MaSP);
			if (cartItem == null)
			{
				cartItem = new Cart1
				{
					MaSP = sanPham.MaSP,
					CartId = ShoppingCartId,
					Count = 1,
					NgayTao = DateTime.Now
				};
				db.CARTs1.Add(cartItem);
			}
			else
			{
				cartItem.Count++;
			}
			db.SaveChanges();
		}

		public int RemoveFromCart(int id)
		{
			Cart1 cartItem = db.CARTs1.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);

			int itemCount = 0;

			if (cartItem != null)
			{
				if (cartItem.Count > 1)
				{
					cartItem.Count--;
					itemCount = cartItem.Count;
				}
				else
				{
					db.CARTs1.Remove(cartItem);
				}

				db.SaveChanges();
			}

			return itemCount;
		}
		
		public void EmptyCart()
		{
			var cartItems = db.CARTs1.Where(cart => cart.CartId == ShoppingCartId);

			foreach (var cartItem in cartItems)
			{
				db.CARTs1.Remove(cartItem);
			}

			db.SaveChanges();
		}

		public List<Cart1> GetCartItems()
		{
			return db.CARTs1.Where(cart => cart.CartId == ShoppingCartId).ToList();
		}

		public int GetCount()
		{
			int? count = (from cartItems in db.CARTs1
						  where cartItems.CartId == ShoppingCartId
						  select (int?)cartItems.Count).Sum();

			return count ?? 0;
		}

		public decimal GetTotal()
		{
			decimal? total = (from cartItems in db.CARTs1
							  where cartItems.CartId == ShoppingCartId
							  select (int?)cartItems.Count * cartItems.SanPham.GiaBan).Sum();
			return total ?? decimal.Zero;
		}

		public string TaoDonHang(DONHANG donHang)
		{
			decimal TongTien = 0;
			var cartItems = GetCartItems();

			foreach (var item in cartItems)
			{
				var ctDonHang = new CHITIETDONHANG
				{
					MaSP = item.MaSP,
					MaDH = donHang.MaDH,
					DonGia = item.SanPham.GiaBan,
					SoLuong = item.Count
				};

				TongTien += (item.Count * item.SanPham.GiaBan) ?? 0;

				db.CHITIETDONHANGs.Add(ctDonHang);
			}

			donHang.TongTien = TongTien;

			db.SaveChanges();
			EmptyCart();
			return donHang.MaDH;
		}
		
		public void MigrateCart(string userName)
		{
			if (db.CARTs1 == null) return;
			if ( db.CARTs1.SingleOrDefault(p=>p.CartId == userName) == null) return;
			var shoppingCart = db.CARTs1.Where(c => c.CartId == ShoppingCartId);
			//if (shoppingCart == null) return;
				
			foreach (Cart1 item in shoppingCart)
			{
				item.CartId = userName;
			}
			db.SaveChanges();
		}

		private string GetCartId(HttpContextBase context)
		{
			if (context.Session[CartSessionKey] == null)
			{
				if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
				{
					context.Session[CartSessionKey] = context.User.Identity.Name;
				}
				else
				{
					Guid tempCartId = Guid.NewGuid();
					context.Session[CartSessionKey] = tempCartId.ToString();
				}
			}

			return context.Session[CartSessionKey].ToString();
		}
	}
}