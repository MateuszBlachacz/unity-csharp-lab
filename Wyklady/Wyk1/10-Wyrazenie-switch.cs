// Wykład 1, slajd: Wyrażenie switch
// Fragment z wykładu do czytania i przepisywania; nie jest częścią projektu Unity.

int value = pickup switch
{
    PickupType.Coin => 1,
    PickupType.Gem  => 10,
    PickupType.Star => 25,
    _               => 0,
};

string rank = score switch
{
    >= 1000 => "Mistrz",
    >= 500  => "Weteran",
    _       => "Nowicjusz",
};
