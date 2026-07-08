////using Enums;
////using UnityEngine;

////public class TileGenerator
////{
////    public void Generate(InventoryGrid grid)
////    {
////        int width = grid.Width;
////        int height = grid.Height;


////        int yellowBorder =
////            Mathf.Max(
////                1,
////                Mathf.Min(width, height) / 3
////            );


////        for (int x = 0; x < width; x++)
////        {
////            for (int y = 0; y < height; y++)
////            {
////                TileType type = CalculateTile(
////                    x,
////                    y,
////                    width,
////                    height,
////                    yellowBorder
////                );


////                grid.GetCell(x, y).TileType = type;
////            }
////        }
////    }


////    private TileType CalculateTile(
////        int x,
////        int y,
////        int width,
////        int height,
////        int yellowBorder)
////    {
////        int distanceFromEdge =
////            Mathf.Min(
////                x,
////                y,
////                width - 1 - x,
////                height - 1 - y
////            );


////        // внешний слой
////        if (distanceFromEdge == 0)
////            return TileType.Red;


////        // желтая зона
////        if (distanceFromEdge <= yellowBorder)
////            return TileType.Yellow;


////        // центр
////        return TileType.Green;
////    }
////}

//using Enums;
//using UnityEngine;

//public class TileGenerator
//{
//    public void Generate(InventoryGrid grid)
//    {
//        int width = grid.Width;
//        int height = grid.Height;


//        int yellowLayers = 1;


//        for (int x = 0; x < width; x++)
//        {
//            for (int y = 0; y < height; y++)
//            {
//                grid.GetCell(x, y).TileType =
//                    CalculateTile(
//                        x,
//                        y,
//                        width,
//                        height,
//                        yellowLayers
//                    );
//            }
//        }
//    }


//    private TileType CalculateTile(
//        int x,
//        int y,
//        int width,
//        int height,
//        int yellowLayers)
//    {
//        int distance =
//            Mathf.Min(
//                x,
//                y,
//                width - 1 - x,
//                height - 1 - y
//            );


//        if (distance == 0)
//            return TileType.Red;


//        if (distance <= yellowLayers)
//            return TileType.Yellow;


//        return TileType.Green;
//    }
//}

using UnityEngine;

public class TileGenerator
{
    private readonly TileDefinition _green;
    private readonly TileDefinition _yellow;
    private readonly TileDefinition _red;


    public TileGenerator(
        TileDefinition green,
        TileDefinition yellow,
        TileDefinition red)
    {
        _green = green;
        _yellow = yellow;
        _red = red;
    }


    public void Generate(InventoryGrid grid)
    {
        int width = grid.Width;
        int height = grid.Height;


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.GetCell(x, y).Tile =
                    CalculateTile(
                        x,
                        y,
                        width,
                        height
                    );
            }
        }
    }


    private TileDefinition CalculateTile(
        int x,
        int y,
        int width,
        int height)
    {
        int distance =
            Mathf.Min(
                x,
                y,
                width - 1 - x,
                height - 1 - y
            );


        if (distance == 0)
            return _red;


        if (distance == 1)
            return _yellow;


        return _green;
    }
}