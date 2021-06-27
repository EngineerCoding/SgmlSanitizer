<a href="https://www.nuget.org/packages/Sgm;Sanitizer#">
    <img src="https://img.shields.io/nuget/v/SgmlSanitizer?style=flat-square" alt="Get on NuGet!">
</a>

# SgmlSanitizer

A sanitizer to remove elements and attributes from SGML based text files, such as HTML and XML. Note that this is a very simple implementation, but by using whitelists and a naïve URL detection implementation XSS may be prevented. Use at your own risk though.

### Usage

To sanitize some simple HTML to remove all attributes and only keep the original HTML tag (this is inserted if not provided) and A tags:

``` csharp
string html = "<html><a href=\"https://url\">Keep me</a><p><a>Don't keep me</a></p></html>";

ListSanitiationHandler list = new ListSanitationHandler();
list.Elements.Add("html");
list.Elements.Add("a");

Sanitizer sanitizer = new Sanitizer();
sanitizer.Handlers.Add(list);

string cleanHtml = sanitizer.Sanitize(html);
Console.WriteLine(cleanHtml);
```

This outputs:

```
<html><a href="https://url">Keep me</a></html>
```

The list sanitation handler can be used to whitelist or blacklist elements, attributes and element specific attributes. The `UrlAttributeHandler` will remove attributes which contain '.' or '/', as the definition of an url is very broad.
