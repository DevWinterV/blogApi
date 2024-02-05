using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WEB_API.Dtos.Post;
using WEB_API.Models;
using WEB_API.Services.FileUpload;

namespace WEB_API.Services.PostService
{
    public class PostService : IPostService
    {
        private IMapper _mapper;
        private readonly ContactContext _context;

        public PostService(ContactContext context, IMapper mapper) {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServerBaseReponse<int>> createPost(PostRequest request)
        {

            var results = new ServerBaseReponse<int>();
            try
            {
                var newAdd = _mapper.Map<News>(request);
                _context.Newss.Add(newAdd);
                await _context.SaveChangesAsync();
                results.Data = newAdd.Id;
                // Set success and message
                results.Success = true;
                results.Message = "OK";

                return results;
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error in GetAllPost: {ex.Message}");

                // Set failure and error message
                results.Success = false;
                results.Message = "An error occurred while fetching posts.";

                // Return the response
                return results;
            }
        }

        public async Task<ServerBaseReponse<PostResponse>> getpostById(int id)
        {
            var results = new ServerBaseReponse<PostResponse>();
            try
            {
                // Retrieve the list of posts along with their images
                var post = await _context.Newss.FirstOrDefaultAsync(x => x.Id.Equals(id));
                results.Data = _mapper.Map<PostResponse>(post);
                // Set success and message
                results.Success = true;
                results.Message = "OK";

                return results;
            }
            catch (Exception ex)
            {
                results.Data = null;
                // Set failure and error message
                results.Success = false;
                results.Message = "Post Not Found";
                // Return the response
                return results;
            }
        }
        public async Task<ServerBaseReponse<List<PostResponse>>> getAllPost(int cursor, int limit)
        {
            var results = new ServerBaseReponse<List<PostResponse>>();
            try
            {
                // Retrieve the list of posts along with their images
                var listPost = await _context.Newss.Include(x => x.Images).OrderByDescending(x => x.PublishDate).ToListAsync();

                // Calculate the total count of posts
                results.paging.total = listPost.Count;

                // Check if there are posts to return based on cursor and limit
                if (cursor < results.paging.total)
                {
                    results.Data = listPost.Skip(cursor).Take(limit).Select(x => _mapper.Map<PostResponse>(x)).ToList();

                    // Check if there are more posts to fetch
                    if ((cursor + limit) < results.paging.total)
                    {
                        results.paging.hasNext = true;
                        results.paging.nextCursor = cursor + limit;
                        results.paging.cursor = cursor + limit;
                    }
                    else
                    {
                        results.paging.hasNext = false;
                        results.paging.nextCursor = 0;
                        results.paging.cursor = 0;
                    }
                }
                else
                {
                    results.Data = new List<PostResponse>();
                    results.paging.hasNext = false;
                    results.paging.nextCursor = 0;
                }

                results.paging.limit = limit;

                // Set success and message
                results.Success = true;
                results.Message = "OK";

                return results;
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error in GetAllPost: {ex.Message}");

                // Set failure and error message
                results.Success = false;
                results.Message = "An error occurred while fetching posts.";

                // Return the response
                return results;
            }
        }

        public async Task<ServerBaseReponse<bool>> deletePost(int id)
        {
            var results = new ServerBaseReponse<bool>();
            try
            {
                // Retrieve the list of posts along with their images
                var post = await _context.Newss.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if(post == null)
                {
                    results.Data = false;
                    results.Success = false;
                    results.Message = "NewsNotFound";
                    return results;
                }

                _context.Newss.Remove(post);
                await _context.SaveChangesAsync();
                // Set success and message
                results.Success = true;
                results.Message = "OK";
                return results;
            }
            catch (Exception ex)
            {
                results.Data = false;
                // Set failure and error message
                results.Success = false;
                results.Message = "ErrorCreate";
                // Return the response
            }
            return results;

        }
    }
}
