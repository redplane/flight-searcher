using AutoMapper;

namespace MainMicroService.Models
{
    public class MappingProfile : Profile
    {
        #region Constructor

        /// <summary>
        ///     Initialize automapper mapping profile.
        /// </summary>
        public MappingProfile()
        {
            // Post mapping.
            //CreateMap<AddPostViewModel, Post>();
        }

        #endregion
    }
}