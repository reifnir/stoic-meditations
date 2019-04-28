
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

## Functions
(TimerTrigger -> every hour)
CheckForNewPodcasts
    var podcasts = await GetRemotePodcastIds() ?? new List();
    var knownPodcasts = await GetKnownPodcastIds() ?? new List();
    var newPodcasts = podcasts.Except(knownPodcasts);
    await newPodcasts.ForEach(IdentifyNewPodcast);


GetRemotePodcastIds
    var json = await GetJsonFromAnchor(https://anchor.fm/stoicmeditations/);
    var podcastIds = (LINQ to interrogate the structure and just return the unique identifiers);
    return podcastIds;


GetKnownPodcastIds()

GetKnownPodcastIds
IdentifyNewPodcast

-----------------------------------------------------------



