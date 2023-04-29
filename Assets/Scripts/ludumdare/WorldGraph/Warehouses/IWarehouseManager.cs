using System.Collections.Generic;
using LudumDare.WorldGraph.Warehouses.Impl;

namespace LudumDare.WorldGraph.Warehouses
{
    public interface IWarehouseManager
    {
        IEnumerable<Warehouse> GetAll();
    }
}