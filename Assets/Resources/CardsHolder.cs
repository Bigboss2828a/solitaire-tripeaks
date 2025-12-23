using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CardHolder",
    menuName = "Cards/Card"
)]
public class CardsHolder : ScriptableObject
{
    public List<CardModel> spadeCards;
    public List<CardModel> heartCards;
}
