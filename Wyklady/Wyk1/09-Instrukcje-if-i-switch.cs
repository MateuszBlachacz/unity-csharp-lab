// Wykład 1, slajd: Instrukcje if i switch
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

if (hp <= 0) { Die(); }
else if (hp < 30) { ShowWarning(); }
else { HideWarning(); }

switch (pickup)            // pickup: enum PickupType
{
    case PickupType.Coin:
        score += 1;
        break;
    case PickupType.Gem:
    case PickupType.Star:  // kilka etykiet, jedna akcja
        score += 10;
        break;
    default:
        Debug.LogWarning("Nieznany przedmiot");
        break;
}
