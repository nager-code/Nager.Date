[![Build, Test & Publish](https://github.com/nager/Nager.Date/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nager/Nager.Date/actions/workflows/dotnet.yml)

# :calendar: Nager.Date - [Official Website](https://nagerholidays.com)

Discover the convenience of easily accessing holidays from **over 200 countries** with Nager.Date. Our popular project utilizes the power of .NET and offers a user-friendly public [REST API](https://nagerholidays.com/api) for seamless integration into your application.

You can find an overview of the supported countries [here](https://nagerholidays.com/country/coverage).

Need offline access to our functionality? No problem! We also provide solutions that allow you to use our services on your own infrastructure without an internet connection. Easily integrate our service into your system with the [Docker](https://hub.docker.com/r/nager/nager-date) container or the [NuGet](https://www.nuget.org/packages/Nager.Date) package. Both options require a license key. As a [sponsor of nager](https://github.com/sponsors/nager), you get a license key.

## Project Scope

Nager.Date provides the **days on which normal working life is interrupted** — public holidays, bank holidays, closures of authorities and school holidays. The data is meant to drive automated business decisions, for example:

- switching a telephone system to holiday mode / enabling the answering machine
- planning deliveries, logistics and supply chains
- staff scheduling, absence planning and time recording

Nager.Date is **not** meant to be a complete calendar of commemorative, cultural, professional or international days. Keeping the dataset focused keeps it reliable: every entry has to be maintained and verified for every year, and too many days without an effect on working life make the data noisier for the people who use it to automate their business.

### Inclusion criteria

A day is included if **at least one** of the following applies:

| Criterion                                                                                          | Holiday type  |
| -------------------------------------------------------------------------------------------------- | ------------- |
| Statutory day off by law or decree for the whole country or a first-level subdivision (ISO 3166-2) | `Public`      |
| Banks and financial institutions are closed                                                        | `Bank`        |
| Public authorities are closed                                                                      | `Authorities` |
| Schools are closed (school holidays) — parents often take leave and are missing at work            | `School`      |
| Not statutory, but most businesses close or most employees take the day off in practice           | `Optional`    |

**Rule of thumb:** *Does a company have to change how it operates on this day?*

### Observances

Days without a day off can be added as `Observance`, but only **selectively**. An observance should:

- be officially established in the country (law, decree or government), not only by an association or organization
- be widely known and noticeable in everyday life of the country (e.g. Mother's Day for retail)
- have a meaning for the country itself, not only be an international or UN day

The holidays with an effect on working life must remain the core of every country. As a guideline, a country should **not have more observances than public holidays**.

The following days are usually **not accepted**:

- Professional and awareness days (e.g. Teachers' Day, Physicians' Day, Police Day, Book Day)
- Anniversaries of organizations, parties or armed forces
- Long lists of commemorative and remembrance days
- Holidays of cities or regions below the first subdivision level (see [Data precision](#data-precision))

Please open an issue first if you are unsure whether a day qualifies.

## How can I use it?

| Variant                                                               | Hosting           | Internet required | License key | Examples                                          |
| --------------------------------------------------------------------- | ----------------- | ----------------- | ----------- | ------------------------------------------------- |
| [REST API](https://nagerholidays.com/api)                             | nagerholidays.com | Yes               | No          | [REST API examples](doc/examples/rest-api.md)     |
| [Docker container](https://hub.docker.com/r/nager/nager-date)         | Self-hosted       | No                | Yes         | [REST API examples](doc/examples/rest-api.md)     |
| [NuGet package Nager.Date](https://www.nuget.org/packages/Nager.Date) | In your .NET app  | No                | Yes         | [Offline examples](doc/examples/offline-nuget.md) |

The Docker container provides **the same REST API** as nagerholidays.com, but runs on your own infrastructure. Existing integrations only need a different base URL.

### REST API

```bash
curl https://nagerholidays.com/api/v4/Holidays/US/2026
```

The [API documentation](https://nagerholidays.com/scalar/#community-api-v4) provides a complete overview of all available endpoints.
You can generate a client in your preferred programming language with the [OpenAPI Generator](https://openapi-generator.tech).
For .NET there is the official client [Nager.Holiday](https://www.nuget.org/packages/Nager.Holiday).

Examples for .NET, Python, JavaScript, PHP and Java: [doc/examples/rest-api.md](doc/examples/rest-api.md)

## Offline Solution

Don't let internet connectivity issues disrupt your workflow. Our offline solutions enable you to use our services on your own infrastructure without an internet connection. With a [sponsorship](https://github.com/sponsors/nager) you get the license key to use the variants locally without a dependency to our REST API.

### NuGet

```bash
dotnet add package Nager.Date
```

```cs
HolidaySystem.LicenseKey = "TheLicenseKey";

var holidays = HolidaySystem.GetHolidays(2026, CountryCode.DE);
```

More examples: [doc/examples/offline-nuget.md](doc/examples/offline-nuget.md)

### Docker

The Docker container is available via [Docker Hub](https://hub.docker.com/r/nager/nager-date). It provides the same REST API as nagerholidays.com, operated on your own infrastructure without a dependency on our servers.

```bash
docker run -p 80:8080 nager/nager-date
```

All [REST API examples](doc/examples/rest-api.md) work unchanged, only the base URL changes:

```bash
curl http://localhost/api/v4/Holidays/US/2026
```


## Holiday types
What variants of holidays are supported by `Nager.Date`

| Type        | Description                                 |
| ----------- | ------------------------------------------- |
| Public      | Public holiday                              |
| Bank        | Bank holiday, banks and offices are closed  |
| School      | School holiday, schools are closed          |
| Authorities | Authorities are closed                      |
| Optional    | Majority of people take a day off           |
| Observance  | No day off, selected days only, see [Observances](#observances) |

## Special Date Types (Dynamic & Unconfirmed Dates)

To better handle complex calendar systems and official announcements, the project distinguishes certain holidays with special characteristics:

*   **Roughly Date:** This applies to holidays translated from calendars like the `HijriCalendar`. Because these dates rely on local moon sightings, they are subject to change and may shift 1–2 days forward or backward at short notice.
*   **Tentative Date:** This applies to holidays that are highly likely to occur on the specified date but are still pending official confirmation or proclamation by the respective government.

## Release and Support Lifecycle

Here you can see how long the product will be supported and when the End of Life (EOL) is planned.

| Product                          | Release Date                | Supported | End of life |
| -------------------------------- | --------------------------- | --------- | ----------- |
| Pro - WebApi v1                  | under development           | No        | -           |
| Community - WebApi v4            | 2026-06-30                  | Yes       | -           |
| Community - WebApi v3            | 2021-05-18                  | Yes       | 2027-01-31  |
| Community - WebApi v2            | 2019-02-10                  | No        | 2024-12-01  |
| Community - WebApi v1            | 2017-02-06                  | No        | 2024-06-24  |
| Nuget Package v2                 | 2024-03-07                  | Yes       | -           |
| Nuget Package v1                 | 2014-08-15                  | No        | 2024-03-07  |

## Data precision

There is no generally valid designation for the next administrative level of countries.
"Nager.Date" supports the initial subdivision of a country, but we will not support a detailed level because the effort required is too high.

To keep it generally valid, we will treat this subdivision as `SubdivisionCodes`, this will replace the current designation `Counties`.

- **United States of America** use `States`
- **Germany** use `States`
- **Austria** use `States`
- **Switzerland** use `Cantons`
- **Brazil** use `States`
- **Australia** use `States` or `Territories`
- **Russia** use `Federal districts`
- **Canada** use `Province` or `Territories`

| Precision                                                                    | Supported |
| ---------------------------------------------------------------------------- | --------- |
| Public Holidays for specific Country                                         | Yes       |
| Holidays for Subdivisions based on ISO 3166-2 (first level)                  | Yes       |
| Holidays for Cities                                                          | No        |
| Holidays for Regions                                                         | No        |

## Limitation Regarding Islamic Holidays
Some Islamic holidays (e.g., Eid al-Fitr, Eid al-Adha) are based on the Islamic lunar calendar and are often determined by local moon sightings. As this depends on weather conditions and geographic location, these dates cannot be reliably calculated in advance. Therefore, such holidays are not included in this project.

## Areas of Application

There are several business fields in which it is important to know the holidays in different countries.

- **E-commerce**: If an online retailer sells its products in different countries, it should know the holidays in these countries to adjust delivery times and customer service accordingly.
- **Travel industry:** A tour operator should know the holidays of the countries to which it offers trips, to alert its customers to special events or closed attractions.
- **Staff scheduling:** Companies with branches in several countries must know the holidays in each country to be able to plan their staff needs accordingly. School holidays matter as well, because many parents take leave during these periods.
- **Financial industry:** Banks and financial institutions should know the holidays in the countries in which they conduct business to ensure that transfers and transactions are performed on time and to estimate the impact of holidays on the foreign exchange market.
- **Logistics and supply chains:** Companies managing logistics or supply chains across several countries, must know the holidays in these countries to adjust planning and supply chain decisions.
- **Telephone systems:** To automatically turn on the answering machine on holidays.
- **Time recording:** To automatically fill the missing hours with the normal working hours.

## Articles about this project

- [Mark Seemann - Simple holidays](http://blog.ploeh.dk/2017/04/24/simple-holidays/)
- [dotnetpro - Feiertagsrechner (German)](https://www.developer-world.de/dwx-insights/frameworks-libraries/feiertagsrechner)
