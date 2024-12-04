using System;
using Models;
using Repositories;

namespace Test;
/// <summary>
/// Innehåller alla metoder för att testa OrderRepo i KonsolAppen(Program)
/// </summary>
static class TEST_ORDER
{
    public static void UpdateOrder(OrderRepository orderRepository, Order order)
    {
        if (order.ID <= 0)
        {
            throw new ArgumentException("Ogiltigt eller tomt ID, fyll i korrekt ID\n\n");
        }
        if (orderRepository.GetOrderByID(order.ID) == null)
        {
            throw new ArgumentException($"Ingen order med ID {order.ID} hittades i databasen.\n\n");
        }


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
