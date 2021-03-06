using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Tests
{
  [TestClass]
  public class ProductTest : IDisposable
  {
    public ProductTest()
    {
     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=online_shop_test;";
    }
    public void Dispose()
    {
      Category.DeleteAll();
      Product.DeleteAll();
      Item.DeleteAll();
      Address.DeleteAll();
      Buyer.DeleteAll();
      Order.DeleteAll();
    }
    [TestMethod]
    public void Equals_TrueForSameProductProperties_True()
    {
      //Arrange, Act
     Product firstProduct = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","",1);
     Product secondProduct = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","",1);
     //Assert
     Assert.AreEqual(firstProduct, secondProduct);
    }
    [TestMethod]
    public void Save_SavesProductToDatabase_ProductList()
    {
      //Arrange
      Product product = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","",1);
      product.Save();
      //Act
      List<Product> expected = new List<Product> {product};
      List<Product> actual = Product.GetAll();
      //Assert
      CollectionAssert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      //Arrange
      Product testProduct = new Product(1,"Reebok","Shoes",100.0,"good quality","Amazon","",1);
      testProduct.Save();
      //Act
      Product savedProduct = Product.GetAll()[0];
      int actual = savedProduct.GetId();
      int expected = testProduct.GetId();
      //Assert
      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Find_FindsProductInDatabase_Product()
    {
      //Arrange
      Product expected = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","",1);
      expected.Save();
      //Act
      Product actual = Product.Find(expected.GetId());
      //Assert
      Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    public void Delete_DeletesProductFromDatabase_ProductList()
    {
      //Arrange
      Product testProduct1 = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","",1);
      testProduct1.Save();
      Product testProduct2 = new Product(1,"Reebok","Shoes",100.0,"good quality","Amazon","",1);
      testProduct2.Save();
      //Act
      testProduct1.Delete();
      List<Product> actualProductList = Product.GetAll();
      List<Product> expectedProductList = new List<Product> {testProduct2};
      //Assert
      CollectionAssert.AreEqual(expectedProductList, actualProductList);
    }

    [TestMethod]
    public void Search_SearchesProductsInDBAndReturnsProductListWithMatchingBrand_ProductList()
    {
      Product testProduct1 = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","");
      testProduct1.Save();
      Product testProduct2 = new Product(1,"Reebok","Shoes",100.0,"good quality","Amazon","");
      testProduct2.Save();
      Product testProduct3 = new Product(1,"Adidas","Shoes",100.0,"good quality","Amazon","");
      testProduct3.Save();

      List<Product> expected = new List<Product> {testProduct1, testProduct3};
      List<Product> actual = Product.Search("Adidas");

      CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Search_SearchesProductsInDBAndReturnsProductListWithMatchingName_ProductList()
    {
      Product testProduct1 = new Product(1,"Adidas","Jeans",100.0,"good quality","Amazon","");
      testProduct1.Save();
      Product testProduct2 = new Product(1,"Reebok","Shoes",100.0,"good quality","Amazon","");
      testProduct2.Save();
      Product testProduct3 = new Product(1,"Adidas","Shoes",100.0,"good quality","Amazon","");
      testProduct3.Save();

      List<Product> expected = new List<Product> {testProduct2, testProduct3};
      List<Product> actual = Product.Search("Shoes");

      foreach(var product in expected)
      {
        Console.WriteLine("EXPECTED: " + product.GetBrand() + ", " + product.GetName());
      }

      foreach(var product in actual)
      {
        Console.WriteLine("ACTUAL: " + product.GetBrand() + ", " + product.GetName());
      }

      CollectionAssert.AreEqual(expected, actual);
    }
  }
}
