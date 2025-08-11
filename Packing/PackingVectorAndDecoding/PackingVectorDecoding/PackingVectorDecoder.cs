public class PackingVectorDecoder
{
    private readonly IPackingVectorPartDecoder<PlacementHeuristic> _cellToHeuristicDecoder;
    private readonly IPackingVectorPartDecoder<Rotation> _cellToRotationDecoder;
    private readonly IPackingVectorPartDecoder<ContainerProperties> _cellToContainerDecoder;
    private readonly IBoxToBePackedSorter _boxSorter;
    private readonly PackingInput _packingInput;
    private readonly int _packingVectorPartLength;


    public int PackingVectorMinimalLength { get; init; }


    private int GetNumberOfPackingVectorParts()
    {
        int i = 0;
        if (_cellToHeuristicDecoder.IsUsingPackingVector)
        {
            i++;
        }
        if (_cellToRotationDecoder.IsUsingPackingVector)
        {
            i++;
        }
        if (_cellToContainerDecoder.IsUsingPackingVector)
        {
            i++;
        }
        if (_boxSorter.IsUsingPackingVector)
        {
            i++;
        }
        return i;
    }

    public PackingVectorDecoder(IPackingVectorPartDecoder<PlacementHeuristic> cellToHeuristicDecoder, IPackingVectorPartDecoder<Rotation> cellToRotationDecoder, IPackingVectorPartDecoder<ContainerProperties> cellToContainerDecoder, IBoxToBePackedSorter boxSorter, PackingInput packingInput)
    {
        _cellToHeuristicDecoder = cellToHeuristicDecoder;
        _cellToRotationDecoder = cellToRotationDecoder;
        _boxSorter = boxSorter;
        _packingInput = packingInput;
        _cellToContainerDecoder = cellToContainerDecoder;
        _packingVectorPartLength = _packingInput.BoxPropertiesList.Count;
        PackingVectorMinimalLength = GetNumberOfPackingVectorParts() * _packingVectorPartLength;
    }

    public IReadOnlyList<BoxToBePacked> Decode(PackingVector packingVector)
    {
        BoxToBePacked[] boxesToBePackedUnsorted = ConvertToBoxToBePackedList(packingVector);

        return _boxSorter.Sort(boxesToBePackedUnsorted, GetBoxSortingPart(packingVector));
    }

    public IReadOnlyList<ContainerProperties> DecodeContainers(PackingVector packingVector) 
    {
        return _cellToContainerDecoder.DecodeMultiple(GetContainerPart(packingVector));
       
    }


    private PackingVector GetBoxSortingPart(PackingVector packingVector)
    {
        if (_boxSorter.IsUsingPackingVector)
        {
            return packingVector.Slice(0, _packingVectorPartLength);
        }
        return PackingVector.CreateEmpty();
    }

    private PackingVector GetContainerPart(PackingVector packingVector) 
    {
        if (_cellToContainerDecoder.IsUsingPackingVector)
        {
            return packingVector.Slice(packingVector.Count - _packingVectorPartLength, _packingVectorPartLength);
        }
        return PackingVector.CreateZeros(_packingVectorPartLength);
    }

    private PackingVector GetRotationsPart(PackingVector packingVector) 
    { 
        if(_cellToRotationDecoder.IsUsingPackingVector) 
        {
            int i = 0;
            if (_boxSorter.IsUsingPackingVector)
            {
                i++;
            }
            return packingVector.Slice(i * _packingVectorPartLength, _packingVectorPartLength);

        }
        return PackingVector.CreateZeros(_packingVectorPartLength);

    }

    private PackingVector GetHeuristicsPart(PackingVector packingVector) 
    {
        if (_cellToHeuristicDecoder.IsUsingPackingVector)
        {
            int i = 0;
            if (_boxSorter.IsUsingPackingVector)
            {
                i++;
            }
            if (_cellToRotationDecoder.IsUsingPackingVector)
            {
                i++;
            }
            return packingVector.Slice(i * _packingVectorPartLength, _packingVectorPartLength);

        }
        return PackingVector.CreateZeros(_packingVectorPartLength);
    }

    private BoxToBePacked[] ConvertToBoxToBePackedList(PackingVector packingVector)
    {
        BoxToBePacked[] boxesToBePacked = new BoxToBePacked[_packingVectorPartLength];

        var rotations = _cellToRotationDecoder.DecodeMultiple(GetRotationsPart(packingVector));
        var heuristics = _cellToHeuristicDecoder.DecodeMultiple(GetHeuristicsPart(packingVector));


        for (int i = 0; i < boxesToBePacked.Length;i++)
        {
            boxesToBePacked[i] = new BoxToBePacked(
                _packingInput.BoxPropertiesList[i], 
                rotations[i],
                heuristics[i]
                );
        }
        return boxesToBePacked;
    }

    public static PackingVectorDecoder Create(PackingSetting packingSetting, PackingInput packingInput)
    {
        IPackingVectorPartDecoder<PlacementHeuristic> placementHeuristic;
        if (packingSetting.SelectedPlacementHeuristics.Length == 1)
        {
            placementHeuristic = new OneHeuristicDecoder(PlacementHeuristics.GetPlacementHeuristic(packingSetting.SelectedPlacementHeuristics[0]));
        }
        else
        {
            placementHeuristic = new MultipleHeuristicsDecoder(PlacementHeuristics.GetMultiplePlacementHeuristics(packingSetting.SelectedPlacementHeuristics));
        }

        IPackingVectorPartDecoder<Rotation> rotations;
        if (packingSetting.AllowRotations)
        {
            rotations = new AllRotationsDecoder();
        }
        else
        {
            rotations = new OneRotationDecoder(Rotation.XYZ);
        }



        /* 
          
        IPackingVectorPartDecoder<ContainerProperties> containers;
        if (packingInput.ContainerPropertiesList.Count == 1)
        {
            containers = new OneContainerDecoder(packingInput.ContainerPropertiesList[0]);
        }
        else
        {
            containers = new MultipleContainersDecoder(packingInput.ContainerPropertiesList);
        }
        */

        var containers = new OneContainerDecoder(packingInput.ContainerProperties);



        IBoxToBePackedSorter sorter;
        if (packingSetting.SelectedPackingOrderHeuristic == null)
        {
            sorter = new PackingVectorUsingBoxSorter();
        }
        else
        {
            sorter = new HeuristicalBoxSorter(OrderHeuristics.GetOrderHeuristic(packingSetting.SelectedPackingOrderHeuristic));
        }



        return new PackingVectorDecoder(placementHeuristic, rotations, containers, sorter, packingInput);
    }
}