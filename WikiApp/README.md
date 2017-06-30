#Exercise:

Use a pre-defined location (you can set that to whatever value you like) to get 50 English language
Wikipedia articles nearest to that location. Find the most similar image titles used in those articles.

#Requirements:
- Use C# or F#
- You can define yourself how to measure image title similarity
- Present the results in a very simple UI. For example one text view with a list of most similar image titles.
- Think this as a something that will be deployed to production. Pay attention to readability, modularity and maintainability.

#Hints:

Get nearest 50 English Wikipedia articles by coordinates
https://en.wikipedia.org/w/api.php?action=query&list=geosearch&gsradius=10000&gscoord=37.786971|-122.399677&gslimit=50&format=json

Get images of an Wikipedia article
https://en.wikipedia.org/w/api.php?action=query&prop=images&pageids=18618509&format=json

More information about Wikipedia API
https://www.mediawiki.org/wiki/API:Main_page