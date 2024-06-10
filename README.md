# Newsy

ASP.NET Core és Angular technológiákat használó híraggregátor oldal.

## Description

A felhasználó hírforrásokat vehet fel (egyelőre RSS url-t) és tag-eket.
A hírforrásokra scrape műveletet indíthat.

![sources](https://github.com/gaaaron/Newsy/assets/4689318/cdeaad96-149a-427b-a454-b9c209f9c8eb)

Jelenleg kétféle tag van:
- source tag: hírforráshoz automatikusan létrejön, adott hírforráshoz talált cikkekhez automatikusan hozzáadódik
- contains tag: felhasználó veheti fel. Ha adott szöveg szerepel egy cikk leírásában, akkor a tag a cikkhez rendelődik.

A felhasználó a felvett tag-ek alapján elkészítheti saját feed-jeit.

![feed](https://github.com/gaaaron/Newsy/assets/4689318/02164f6e-fc0e-467d-a32b-4b1a1c4f1a63)

## Projects

- **Newsy.Api** : REST API (ASP.NET Core WebAPI) 
- **Newsy.Application** : Application layer  (.NET Core)
- **Newsy.Client** : client app (Angular v18)
- **Newsy.Domain** : Domain layer (.NET Core)
- **Newsy.Infrastructure** : Infrastructure layer (.NET Core)

## Domain model
![System design](docs/diagrams/out/architecture_simpl/System%20design.png)
