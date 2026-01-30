namespace Phase05MVP4.Services.Trees;
public class DefaultTreesCollected : ITreesCollecting
{
    int ITreesCollecting.TreesCollectedAtTime => 4; //usually 4 regardless of game.
}