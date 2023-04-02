using System;
using System.Collections.Generic;

namespace StoreApp
{
    class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();
        int choice;
        do
        {
            Console.WriteLine("===============================");
            Console.WriteLine("===============================");
            Console.WriteLine("DOTNET STORE BY ICHAA");
            Console.WriteLine("===============================");
            Console.WriteLine("1. Tambah Produk");
            Console.WriteLine("2. Edit Produk");
            Console.WriteLine("3. Tampilkan Semua Produk");
            Console.WriteLine("4. Hapus Produk");
            Console.WriteLine("5. Add Produk ke cart");
            Console.WriteLine("6. Hapus Produk dari cart");
            Console.WriteLine("7. Tampilkan Cart");
            Console.WriteLine("8. Checkout");
            Console.WriteLine("9. Keluar");
            Console.WriteLine("===============================");
            Console.Write("Pilihan: ");
            choice = int.Parse(Console.ReadLine());
            Console.WriteLine("===============================");
            Console.WriteLine("===============================");
            switch (choice)
            {
                case 1:
                    store.AddProduct();
                    break;
                case 2:
                    store.EditProduct();
                    break;
                case 3:
                    store.ShowProducts();
                    break;
                case 4:
                    store.RemoveProduct();
                    break;
                case 5:
                    store.AddToCart();
                    break;
                case 6:
                    store.RemoveFromCart();
                    break;
                case 7:
                    store.ShowCart();
                    break;
                case 8:
                    store.Checkout();
                    break;
                case 9:
                    Console.WriteLine("Keluar");
                    break;
                default:
                    Console.WriteLine("Pilihan Tidak Ada");
                    break;
            }
            Console.WriteLine("===============================");
        } while (choice != 9);
    }
}
    class Product
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

    class CartItem
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    class Store
    {
        private List<Product> products = new List<Product>();
        private List<CartItem> cart = new List<CartItem>();

        public void AddProduct()
        {
            Console.WriteLine("Tambah Produk");
            Console.WriteLine("===============================");
            Console.WriteLine("Masukkan SKUnya:");
            string sku = Console.ReadLine();
            if (ProductExists(sku))
            {
                Console.WriteLine("SKU produk {0} sudah ada.", sku);
                return;
            }
            Console.WriteLine("Masukkan Nama:");
            string name = Console.ReadLine();
            Console.WriteLine("Masukkan Stok:");
            int stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Masukkan Harga:");
            decimal price = decimal.Parse(Console.ReadLine());
            products.Add(new Product { SKU = sku, Name = name, Stock = stock, Price = price });
            Console.WriteLine("===============================");
            Console.WriteLine("Produk sudah ditambahkan :)");
        }

        public void EditProduct()
        {
            Console.WriteLine("Edit Produk");
            Console.WriteLine("===============================");
            Console.WriteLine("Masukkan SKU:");
            string sku = Console.ReadLine();
            Product product = products.Find(p => p.SKU == sku);
            if (product == null)
            {
                Console.WriteLine("Sku produk {0} tidak ditemukan.", sku);
                return;
            }
            Console.WriteLine("Masukkan Nama:");
            string name = Console.ReadLine();
            Console.WriteLine("Masukkan Stok:");
            int stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Masukkan Harga:");
            decimal price = decimal.Parse(Console.ReadLine());
            product.Name = name;
            product.Stock = stock;
            product.Price = price;
            Console.WriteLine("===============================");
            Console.WriteLine("Produk telah diedit.");
        }

        public void ShowProducts()
        {
            Console.WriteLine("List Produk");
            Console.WriteLine("===============================");
            Console.WriteLine("SKU\tNama\tStok\tHarga");
            foreach (Product product in products)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", product.SKU, product.Name, product.Stock, product.Price);
            }
        }

        public void RemoveProduct()
        {
            Console.WriteLine("Hapus Produk");
            Console.WriteLine("===============================");
            Console.WriteLine("Enter SKU:");
            string sku = Console.ReadLine();
            Product product = products.Find(p => p.SKU == sku);
            if (product == null)
            {
                Console.WriteLine("Sku produk {0} tidak ditemukan.", sku);
                return;
            }
            products.Remove(product);
            Console.WriteLine("===============================");
            Console.WriteLine("Produk telah terhapus");
        }

        public void AddToCart()
        {
            Console.WriteLine("Masukkan Produk ke Cart");
            Console.WriteLine("===============================");
            Console.WriteLine("Masukkan SKU:");
            string sku = Console.ReadLine();
            Product product = products.Find(p => p.SKU == sku);
            if (product == null)
            {
                Console.WriteLine("SKU Produk {0} tidak ditemukan.", sku);
                return;
            }
            Console.WriteLine("Masukkan Quality:");
            int quantity = int.Parse(Console.ReadLine());
            if (quantity > product.Stock)
            {
                Console.WriteLine("Stok tak cukup.");
                return;
            }
            product.Stock -= quantity;
            cart.Add(new CartItem { SKU = product.SKU, Name = product.Name, Quantity = quantity, Price = product.Price });
            Console.WriteLine("===============================");
            Console.WriteLine("Produk telah ada di cart.");
        }
        public void RemoveFromCart()
        {
            Console.WriteLine("Hapus Produk dari Cart");
            Console.WriteLine("===============================");
            Console.WriteLine("Masukkan SKU:");
            string sku = Console.ReadLine();
            CartItem cartItem = cart.Find(ci => ci.SKU == sku);
            if (cartItem == null)
            {
                Console.WriteLine("SKU Produk {0} tidak ditemukan", sku);
                return;
            }
            Product product = products.Find(p => p.SKU == sku);
            product.Stock += cartItem.Quantity;
            cart.Remove(cartItem);
        Console.WriteLine("===============================");
        Console.WriteLine("Produk terhapus dari Cart");
}

    public void ShowCart()
    {
        Console.WriteLine("List Cart");
        Console.WriteLine("===============================");
        Console.WriteLine("SKU\tNama\tQuantity\tHarga");
        Console.WriteLine("===============================");
        foreach (CartItem cartItem in cart)
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", cartItem.SKU, cartItem.Name, cartItem.Quantity, cartItem.Price);
        }
    }

    public void Checkout()
    {
        Console.WriteLine("Checkout");
        Console.WriteLine("===============================");
        Console.WriteLine("SKU\tNama\tQuantity\tHarga\tTotal");
        Console.WriteLine("===============================");
        decimal total = 0;
        foreach (CartItem cartItem in cart)
        {
            decimal subtotal = cartItem.Quantity * cartItem.Price;
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", cartItem.SKU, cartItem.Name, cartItem.Quantity, cartItem.Price, subtotal);
            total += subtotal;
        }
        Console.WriteLine("===============================");       
        Console.WriteLine("Total:\t\t\t\t\t{0}", total);
        Console.WriteLine("===============================");
        Console.WriteLine("Payment: 1. Bayar, 2. Cancel");
        Console.WriteLine("===============================");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 1)
        {
            cart.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("Payment sukses,");
            Console.WriteLine("terimakasih telah berbelanja di DOTNET STORE BY ICHA^^");
        }
        else if (choice == 2)
        {
            Console.WriteLine("Payment cancelled.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    private bool ProductExists(string sku)
    {
        return products.Exists(p => p.SKU == sku);
    }
}
}