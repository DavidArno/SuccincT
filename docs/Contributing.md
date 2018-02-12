## Contributing to Succinc\<T\> ##
As with most open source projects, I not only welcome contributions to Succinc\<T\>, when I get them it makes the hours spent on it feel worthwhile. It let's me know others know about it, use it and even care about it.

There are numerous ways that contributions can be made; I welcome all of them:

### Tell me that you use it ###
Whilst nuget tells me the number of times the package has been downloaded, it doesn't reveal whether its then used, or soon discarded after trying it. So if you do use it, please tell me about it. Give me a shout on [twitter](https://twitter.com/DavidArno), or [leave a comment on my blog](http://www.davidarno.org/?s=succinct).

### Report bugs and problems; tell me about features you'd like ###
If you try to use Succinc\<T\> and run into a problem, again please tell me. If you find a bug, or simply feel it's missing an important feature, [create an issue on Github](https://github.com/DavidArno/SuccincT/issues) to tell me about it.

### Help with documentation ###
[The wiki is open to all to edit](https://github.com/DavidArno/SuccincT/wiki). If you spot a mistake, or feel you could add a useful explanation or example to the documentation, then please edit away.

### Send me a Pull Request ###
If you feel there's something useful you could add yourself, then please do. Fork and clone the repo, make your changes and then send me a Pull Request (PR).

A few points to note regarding this:

1. Succinc\<T\> uses the MIT licence and I intend to keep it that way. As such, there is no need to sign anything to say you grant me copyright. Your work remains your own and the more contributors I get, the harder it would be to get all of us to agree to switching the licence. By default, I tend not to explicitly acknowledge others' copyright in the licence file, but if you wish me to, tell me and I will. 
2. Please try to supply unit tests if you can. Well tested PR's are easier to approve and merge. If you aren't confident about writing your own tests, let me know and I'll add them, but that will delay things.
3. Please try to follow my coding style. There's no hard and fast rules, but generally if your code looks like mine, things will be good. Again, code in very different styles to my own will be rewritten before being merged, so sticking to the same style speeds things up. 
4. Before starting work, [please check on which branches are currently active](Documents/Branches.md) and only work against those (though PRs from your own branch to one of those active ones are fine).

### Contributors ###
The following folk have already contributed many useful additions to Succinc&lt;T&gt;:
#### [Adam Guest](https://github.com/chamook) ####
* Ability to directly compare `Option<T>` and `Maybe<T>` without casting.

#### [Gregory Bell](https://github.com/Gregory-Bell) ####
* Fix to how hashcodes are calculated in unions

#### [Tony Sedniov](https://github.com/megafinz) ####
* Added caching of Option.None() values
* Union creation performance enhancements
* `Option.Some`, `Option.Choose`, `x.ToOption`, `x.TryCast`, `Option.AsNullable`, `Option.Or`, `Option.Map` and `Option.Flattern` extensions
* Piping into `Action<*>`
* `TryGetValue` for `IDictionary<,>`

#### [Peter Majeed](https://github.com/peter-majeed) ####
* Accessing Union values directly

#### [David Bottiau](https://github.com/Odonno) ####
* `Copy`, `TryCopy`, `With` and `TryWith` methods (coming in v4.0)
