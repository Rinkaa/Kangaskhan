# Kangaskhan || 袋兽 #

<blockquote style="min-width:240px;padding:14px">
    <img align="right" src="https://archives.bulbagarden.net/media/upload/d/d5/Spr_5b_115.png" />
    <span style="color:#00EEEE;">Kangaskhan</span>: Hello, there! Welcome to <span style="color:#00EE00;">Kangaskhan</span> Storage.
    <br/>
    <span style="color:#00EEEE;">袋兽</span>：甜心，欢迎来到<span style="color:#00EE00;">袋兽</span>仓库！
    <div style="indent:39px;">
        <pre style="width:14em;padding:3px;font-size:10px;">▶Store 存放&emsp;&emsp;Take   取出<br/>&emsp;Info  说明&emsp;&emsp;Cancel 取消</pre>
    </div>
</blockquote>

🕹Inventory mechanism class library defined for game programming, written in C# without any dependency on any game engine. Based on `.Net standard 2.0`. Under `MIT License`.

无引擎依赖，用C#语言编写的用于游戏程序的道具系统。基于`.Net standard 2.0`，使用`MIT协议`。

## Contents || 内容 ##

The current version is v0.1

当前版本为v0.1

### Description || 说明 ###

- Inventory item definition, class attribute- and instance component- wise
- 道具定义，可定制类特性与实例组件
- Item stacking
- 道具堆叠
- Inventory storage definition, list-like and cell-like
- 道具仓库定义，包括列表仓库与格子仓库

### Structure || 结构 ###

- [x] InventoryItem
- [x] InventoryItemFactor
- [x] InventoryItemType
- [x] InventoryItemTypeFactor
- [x] Features
    - [x] Stacking
        - [x] StackableTypeFactor
        - [x] InventoryItemStack
- [x] Applications
    - [x] Storages
        - [x] InventoryStorageEntry
        - [x] InventoryStorage{TEntry}
        - [x] ItemStorageEntry
        - [x] StackStorageEntry
        - [x] ItemListStorage
        - [x] ItemCellStorage
        - [x] AutoPurging
            - [x] ValidatableEntry{TInnerEntry}
            - [x] AutoPurgingStorage{TInnerStorage}
            - [x] EmptyCheckedStackStorageEntry
            - [x] StackListStorage
            - [x] StackCellStorage

### TODOs || 已规划 ###

- [ ] Unit tests
- [ ] Sample console app
- [ ] Simple usage guide in `readme.md`
- [ ] Feature: Expiring (a InventoryItem factor, meaning that each instance having this factor has an expire time.
- [ ] Feature: HasFactor (a InventoryItemType factor, meaning that all instances of this kind have a certain factor.)
- [ ] Feature: HasAction (a InventoryItemType factor, meaning that all instances of this kind have a certain action to operate.)

## Open for feature suggestions and pull requests! || 欢迎提出功能建议或提出推送请求！ ##

This project is at its initial stage with a minimal set of features. We would be very grateful if you could help us with ideas about what an Inventory mechanism library should include!

Feel free to leave your suggestions at the Issue page and mark them as "feature-suggestion". <3

项目仍在初期，功能比较简朴，如能助力此项目成长，我们不胜感激！

您可以在Issue页留下您的建议并将其注为"feature-suggestion"。比心

## Changelog || 更改记录 ##

- v0.1
    - Repo init.
    - 项目初始。
