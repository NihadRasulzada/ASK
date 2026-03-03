using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.Core.Entities;
using App.Core.Interfaces;

namespace App.BL.Services.Implementations;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;

    public VideoService(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task<IEnumerable<VideoResponseDto>> GetAllAsync()
    {
        var videos = await _videoRepository.GetAllAsync();
        return videos.Select(MapToResponseDto);
    }

    public async Task<VideoResponseDto?> GetByIdAsync(Guid id)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        return video is null ? null : MapToResponseDto(video);
    }

    public async Task<VideoResponseDto> CreateAsync(CreateVideoDto dto)
    {
        var video = new Video(dto.Link);
        await _videoRepository.AddAsync(video);
        await _videoRepository.SaveChangesAsync();
        return MapToResponseDto(video);
    }

    public async Task<VideoResponseDto?> UpdateAsync(Guid id, UpdateVideoDto dto)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video is null)
            return null;

        video.Update(dto.Link);
        _videoRepository.Update(video);
        await _videoRepository.SaveChangesAsync();
        return MapToResponseDto(video);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var video = await _videoRepository.GetByIdAsync(id);
        if (video is null)
            return false;

        await _videoRepository.DeleteAsync(id);
        await _videoRepository.SaveChangesAsync();
        return true;
    }

    private static VideoResponseDto MapToResponseDto(Video video)
        => new(video.Id, video.Link);
}
