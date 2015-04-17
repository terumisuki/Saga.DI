using System.Collections.Generic;
using Saga.BusinessLayer;
using Saga.Dal;
using Saga.Specification.Interfaces;
using Saga.Specification.Interfaces.Artists;
using Saga.Specification.Interfaces.Audio;
using Saga.Specification.Interfaces.Errors;
using Saga.Specification.Interfaces.Genres;
using Saga.Specification.Interfaces.Images;
using Saga.Specification.Interfaces.Musical;
using Saga.Specification.Interfaces.PhotoAlbums;
using Saga.Specification.Interfaces.Tags;
using Saga.Specification.Interfaces.Users;
using Saga.Specifications.Interfaces.Parts;

namespace Saga.DI
{
    public static class Factory
    {
        #region users
        public static IUserBusiness GetUserBusiness()
        {
            IUserRepository userRepo = GetUserRepository();
            ITrackRepository trackRepo = GetTrackRepository();
            IUtility utility = GetUtility();
            IErrorRepository errorRepo = GetErrorRepository();
            IUserBusiness userBusiness = new UserBusiness(userRepo, trackRepo, utility, errorRepo);
            return userBusiness;
        }

        private static IUserRepository GetUserRepository()
        {
            IGenreRepository genreRepo = GetGenreRepository();
            ITrackRepository trackRepo = GetTrackRepository();
            IArtistRepository artistRepo = GetArtistRepository();
            IPartRepository partRepo = GetPartRepository();
            ITagRepository tagRepo = GetTagRepository();
            IUserRepository userRepo = new UserRepository(genreRepo, trackRepo, artistRepo, partRepo, tagRepo);
            return userRepo;
        }

        private static IPartRepository GetPartRepository()
        {
            return new PartRepository(GetArtistRepository());
        }

        public static IUser GetUser(string username, string password)
        {
            IUserRepository userRepo = GetUserRepository();
            IUser user = userRepo.Get(username, password);
            return user;
        }
        public static IUser GetUser(int userid)
        {
            IUserRepository userRepo = GetUserRepository();
            IUser user = userRepo.Get(userid);
            return user;
        }
        #endregion



        #region utility
        public static IUtility GetUtility()
        {
            IUtility utility = new Utility();
            return utility;
        }
        #endregion



        #region genres
        public static IGenreBusiness GetGenreBusiness()
        {
            IGenreRepository repo = GetGenreRepository();
            IGenreBusiness business = new GenreBusiness(repo);
            return business;
        }

        private static IGenreRepository GetGenreRepository()
        {
            IGenreRepository repo = new GenreRepository();
            return repo;
        }
        #endregion



        #region tracks
        public static ITrack GetTrack(int mediaId)
        {
            ITrackRepository trackRepo = GetTrackRepository();
            ITrack track = trackRepo.GetById(mediaId);
            return track;
        }

        private static ITrackRepository GetTrackRepository()
        {
            IGenreRepository genreRepo = GetGenreRepository();
            IAlbumRepository albumRepo = GetAlbumRepository();
            IPartRepository partRepo = GetPartRepository();
            ITrackRepository trackRepo = new TrackRepository(genreRepo, albumRepo, partRepo);
            return trackRepo;
        }

        public static ITrackBusiness GetTrackBusiness()
        {
            ITrackRepository trackRepo = GetTrackRepository();
            ITrackBusiness trackBus = new TrackBusiness(trackRepo);
            return trackBus;
        }
        #endregion



        #region albums
        public static IAlbumRepository GetAlbumRepository()
        {
            IAlbumRepository albumRepo = new AlbumRepository();
            return albumRepo;
        }
        
        public static IAlbumBusiness GetAlbumBusiness()
        {
            IAlbumRepository albumRepo = GetAlbumRepository();
            ITrackRepository trackRepo = GetTrackRepository();
            IAlbumBusiness albumBus = new AlbumBusiness(albumRepo, trackRepo);
            return albumBus;
        }
        #endregion



        #region errors
        private static IErrorRepository GetErrorRepository()
        {
            IErrorRepository errorRepo = new ErrorRepository();
            return errorRepo;
        }
        #endregion



        #region parts
        public static IPartBusiness GetPartsBusiness()
        {
            IArtistRepository artistRepo = GetArtistRepository();
            IPartRepository partRepo = GetPartRepository();
            PartBusiness partBus = new PartBusiness(partRepo);
            return partBus;
        }
        #endregion



        #region images
        private static IImageRepository GetImageRepository()
        {
            IImageRepository repo = new ImageRepository();
            return repo;
        }
        public static IImageBusiness GetImageBusiness()
        {
            IImageRepository imageRepo = GetImageRepository();
            IErrorRepository errorRepo = GetErrorRepository();
            IImageBusiness imageBus = new ImageBusiness(imageRepo, errorRepo);
            return imageBus;
        }
        #endregion



        #region tags
        private static ITagRepository GetTagRepository()
        {
            ITagRepository repo = new TagRepository();
            return repo;
        }
        public static ITagBusiness GetTagBusiness()
        {
            ITagRepository repo = GetTagRepository();
            ITagBusiness bus = new TagBusiness(repo);
            return bus;
        }
        #endregion


        
        #region artists
        private static IArtistRepository GetArtistRepository()
        {
            IArtistRepository repo = new ArtistRepository();
            return repo;
        }
        public static IArtistBusiness GetArtistBusiness()
        {
            IArtistRepository repo = GetArtistRepository();
            IArtistBusiness bus = new ArtistBusiness(repo);
            return bus;
        }
        #endregion



        #region photos
        public static IImage GetImageObject()
        {
            IImage image = new Image();
            image.Tags = new List<ITag>();
            return image;
        }

        public static IPhotoAlbum GetPhotoAlbumObject()
        {
            IPhotoAlbum album = new PhotoAlbum();
            return album;
        }


        public static IPhotoAlbumBusiness GetPhotoAlbumBusiness()
        {
            IImageBusiness imageBusiness = GetImageBusiness();
            IPhotoAlbumRepository repo = new PhotoAlbumRepository(imageBusiness);
            IPhotoAlbumBusiness buss = new PhotoAlbumsBusiness(repo, imageBusiness);
            return buss;
        }

        public static IPhoto GetPhotoObject(string filePath)
        {
            IPhoto photo = new Photo();
            photo.MediaFilePath = filePath;
            return photo;
        }
        #endregion
    }
}