[System.Serializable]
public class MaxScore
{
	public int MaxScoreToSave;

	public int Soundtrack;

	public float MusicVolume;

	public float SoundsVolume;

	public MaxScore(float music, float sounds, int clip, int score)
	{
		if (score > MaxScoreToSave)
		{
			MaxScoreToSave = score;
		}
		MusicVolume = music;
		SoundsVolume = sounds;
		Soundtrack = clip;
	}
}
