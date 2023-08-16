using System;
using System.Collections.Generic;

namespace LapShop.Models;

public partial class TbSeting
{
    public int Id { get; set; }

    public string WebsiteName { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string WebsiteDescription { get; set; } = null!;

    public string FacebookLink { get; set; } = null!;

    public string TwitterLink { get; set; } = null!;

    public string YoutubeLink { get; set; } = null!;

    public string InstagramLink { get; set; } = null!;

    public string MiddlePanner { get; set; } = null!;
}
