using System;
using Models;
using Repositories;

namespace Test;
static class TEST_ORDER
{
    public static void UpdateOrder(OrderRepository orderRepository, Order order)
    {
        orderRepository.UpdateOrder(order);
        Console.WriteLine($"Ordern har uppdaterats:\n{order}");
    }

    public static void GetOrders(OrderRepository orderRepository)
    {
        var orders = orderRepository.GetOrders();
        if (orders != null && orders.Count > 0)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }
        else
        {
            Console.WriteLine("Inga ordrar hittades i databasen.");
        }
    }

    public static void DeleteOrderByID(OrderRepository orderRepository, int id)
    {
        Order? order = orderRepository.GetOrderByID(id);

        if (order != null)
        {
            Console.WriteLine($"{order}\n\nOrdern har deletats från databasen");

            orderRepository.DeleteOrderByID(id);
        }
        else
        {
            Console.WriteLine("Ingen order i databasen med detta ID.");
        }
    }

    public static void GetOrderByID(OrderRepository orderRepository, int id)
    {
        Order? order = orderRepository.GetOrderByID(id);

        if (order != null)
        {
            Console.WriteLine(order);
        }
        else
        {
            Console.WriteLine("Ingen order i databasen med detta ID.");
        }
    }

    public static void AddOrder(OrderRepository orderRepository, Order order)
    {
        orderRepository.AddOrder(order);
        Console.WriteLine($"{order}\n\nOrdern har lagts till i databasen.");
    }
}
