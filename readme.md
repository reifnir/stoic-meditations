
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
* Speech parsing mechanism
  * RA: Speech services API accessor
* Persistence of blobs
  * Utility: File access

## Functions Notes
```
FindNewPodcasts
    Input: Timer
    Output: Queues IngestPodcast commands
    ->
        var podcasts = await GetRemotePodcastIds();
        var knownPodcasts = await GetKnownPodcastIds();
        var newPodcasts = podcasts.Except(knownPodcasts);
        newPodcasts.ForEach(x => {
            var message = new IngestPodcast() { Id = x };
            queueHelper.Enqueue(message);
        });

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