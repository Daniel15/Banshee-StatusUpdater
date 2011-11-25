using System;
using Banshee.Collection;
using Banshee.MediaEngine;
using Banshee.ServiceStack;
using Banshee.Streaming;
using NTelepathy;

namespace Banshee.StatusUpdater
{
	/// <summary>
	/// Status updater service for Banshee. Updates Telepathy status with
	/// currently playing song.
	/// </summary>
	public class StatusUpdaterService : IExtensionService
	{
		private Status _status = new Status();
		/// <summary>
		/// Format to use for status updates for regular songs
		/// </summary>
		private string _format = "♫ Listening to {0} by {1} [{2}]";
		
		/// <summary>
		/// Initialize this extension
		/// </summary>
		public void Initialize()
		{
			ServiceManager.PlayerEngine.ConnectEvent(OnPlayerEvent, 
				PlayerEvent.StartOfStream |
				PlayerEvent.EndOfStream |
				PlayerEvent.TrackInfoUpdated | 
				PlayerEvent.StateChange);
		}
		
		/// <summary>
		/// Callback for when player events occur
		/// </summary>
		/// <param name='args'>Event arguments</param>
		public void OnPlayerEvent(PlayerEventArgs args)
		{
			// Check what type of event it was - We need to know if playback 
			// was stopped or started.
			switch (args.Event)
			{
				case PlayerEvent.StartOfStream:	
				case PlayerEvent.TrackInfoUpdated:
					UpdateStatus();
					break;
				
				case PlayerEvent.EndOfStream:
					OnStop();
					break;
				
				case PlayerEvent.StateChange:
					OnPlayerStateChange((PlayerEventStateChangeArgs)args);
					break;
			}
		}
		
		/// <summary>
		/// Called when the playback state changes
		/// </summary>
		/// <param name='args'>Event arguments</param>
		private void OnPlayerStateChange(PlayerEventStateChangeArgs args)
		{
			switch (args.Current)
			{
				case PlayerState.Paused:
					OnStop();
					break;
				
				case PlayerState.Playing:
					UpdateStatus();
					break;
			}
		}
		
		/// <summary>
		/// Called when the music has stopped playing.
		/// </summary>
		private void OnStop()
		{
			Console.WriteLine("Music stopped");
			_status.UpdateAllAccounts(string.Empty);
		}
		
		/// <summary>
		/// Updates the Telepathy status.
		/// </summary>
		private void UpdateStatus()
		{
			TrackInfo track = ServiceManager.PlayerEngine.CurrentTrack;
			string album = track.DisplayAlbumTitle;
			
			// If it's a radio station, use the station name as the album name
			if (track is RadioTrackInfo)
			{
				album = ((RadioTrackInfo)track).ParentTrack.TrackTitle;
			}
			
			string message = string.Format(_format, 
				track.DisplayTrackTitle, 
				track.DisplayArtistName,
				album);
			
			Console.WriteLine("Updated: {0}", message);
			
			_status.UpdateAllAccounts(message);
		}
		
		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		public string ServiceName
		{
			get	{ return "StatusUpdaterService"; }
		}
		
		/// <summary>
		/// Releases all resources used by this object.
		/// </summary>
		public void Dispose()
		{
			 ServiceManager.PlayerEngine.DisconnectEvent(OnPlayerEvent);
		}
	}
}

