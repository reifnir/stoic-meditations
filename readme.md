
## Use Cases
* Look for uncaptured podcasts
* Get detailed info about uncaptured podcasts
* Trim out parts of the audio I don't want to hear
  * Remove the intro, outtro music and speech


## Volatilities
* Orchestration of how podcasts and assets are acquired
  * M: Acquision manager
* Consume podcasts
  * M: Library manager
* Mechanism for cutting and splicing audio
  * E: Formatting engine
* Source of podcast data
  * RA: Podcast API accessor
* Persistence of state
  * RA: Catalog access
* Persistence of blobs
  * RA: File access
* Speech parsing mechanism
  * RA: Speech services API accessor

## Functions Notes
```
FindNewPodcasts
    Input: Timer
    Output: Queue: NewPodcast[]
    ->
        var podcasts = await GetRemotePodcastIds();
        var knownPodcasts = await GetKnownPodcastIds();
        var newPodcasts = podcasts.Except(knownPodcasts);
        newPodcasts.ForEach(x =>
            queuedNewPodcastQueue.Enqueue(new NewPodcast(x))
        );

AcquirePodcast (Durable)
    Input: Queue: NewPodcast
    Output: EventGrid: PodcastStateChange
    ->
        var podcast = await GetPodcastStateActivity();
        if (podcast.Locked || podcast.Acquired)
            return;
        await AcquirePodcastDetailsActivity()
        await AcquirePodcastAudioActivity()
        await SetPodcastAsAcquiredActivity()

```