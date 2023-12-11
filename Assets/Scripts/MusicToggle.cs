using UnityEngine;
using UnityEngine.UI;

//vacsina prevzatá z internetu
public class MusicToggle : MonoBehaviour
{
    public AudioSource musicPlayer;  // Reference to the Music Player AudioSource
    public Sprite musicOnIcon;       // Sprite for the Music On icon
    public Sprite musicOffIcon;      // Sprite for the Music Off icon
    private Image buttonImage;       // Reference to the Image component on the button

    void Start()
    {
        // Get the Image component from the button
        buttonImage = GetComponent<Image>();
        UpdateIcon();  // Set the correct icon based on whether music is playing
    }

    public void ToggleMusic()
    {
        // Check if music is playing and toggle its state
        if (musicPlayer.isPlaying)
        {
            musicPlayer.Stop();
        }
        else
        {
            musicPlayer.Play();
        }

        // Update the button icon
        UpdateIcon();
    }

    void UpdateIcon()
    {
        // Change the icon based on the music's state
        if (musicPlayer.isPlaying)
        {
            buttonImage.sprite = musicOnIcon;
        }
        else
        {
            buttonImage.sprite = musicOffIcon;
        }
    }
}