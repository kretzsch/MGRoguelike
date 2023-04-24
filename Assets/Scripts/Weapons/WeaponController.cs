using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum Genre
    {
        TopDown,
        Platformer,
        FPS,
        Invader
    }

    [SerializeField] private Genre gameGenre;
    [SerializeField] private WeaponData weaponData;

    public void Initialize(WeaponData weaponData, Genre genre)
    {
        this.weaponData = weaponData;
        this.gameGenre = genre;

        // Set up the weapon visuals, sounds, etc., using weaponData
    }

    public void Fire()
    {
        if (weaponData == null) return;

        switch (gameGenre)
        {
            case Genre.TopDown:
                // Implement top-down firing logic
                break;
            case Genre.Platformer:
                // Implement platformer firing logic
                break;
            case Genre.FPS:
                break;
            case Genre.Invader:
                break;
        }
    }
}
