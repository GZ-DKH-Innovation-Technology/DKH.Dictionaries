using DKH.Dictionaries.Application.Queries.OverPass.Dto;
using MediatR;
using Newtonsoft.Json;

namespace DKH.Dictionaries.Application.Queries.OverPass;

public class GetOverPassQuery : IRequest<GetOverPassResult>
{
    public GetOverPassQuery(string url, string query)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        Query = query ?? throw new ArgumentNullException(nameof(query));
    }

    public string Url { get; private set; } = string.Empty;
    public string Query { get; private set; } = string.Empty;
}


public class GetOverPassQueryHandler : IRequestHandler<GetOverPassQuery, GetOverPassResult>
{
    private readonly HttpClient _httpClient;
    
    public GetOverPassQueryHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<GetOverPassResult> Handle(GetOverPassQuery request, CancellationToken cancellationToken)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, request.Url)
        {
            Content = new StringContent(request.Query)
        };
        using var response = await _httpClient.SendAsync(message, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<GetOverPassResult>(content) ?? throw new ArgumentNullException(nameof(content));
    }
}