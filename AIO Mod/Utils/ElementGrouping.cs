using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO_Mod.Utils
{
    internal class ElementGrouping
    {
        public Tag GroupTag { get; private set; }

        public List<Element> GroupedElements { get; private set; }

        public ElementGrouping Exclude(Func<Element, bool> predicate)
        {
            this.GroupedElements.RemoveAll((Predicate<Element>)(e => predicate(e)));
            return this;
        }

        public ElementGrouping Include(Func<Element, bool> predicate, bool addGroupTagToElement = true)
        {
            foreach (Element element in ElementLoader.elements.Where<Element>(predicate).ToList<Element>())
            {
                if (!this.GroupedElements.Contains(element))
                {
                    if (addGroupTagToElement && !((IEnumerable<Tag>)element.oreTags).Contains<Tag>(this.GroupTag))
                        element.oreTags = Util.Append<Tag>(element.oreTags, this.GroupTag);
                    this.GroupedElements.Add(element);
                }
            }
            return this;
        }

        public static ElementGrouping GroupAllWith(Tag groupTag)
        {
            return new ElementGrouping()
            {
                GroupTag = groupTag,
                GroupedElements = ElementLoader.elements.Where<Element>((Func<Element, bool>)(e => e.HasTag(groupTag))).ToList<Element>()
            };
        }

        public static implicit operator Tag(ElementGrouping info) => info.GroupTag;

        public static implicit operator List<Tag>(ElementGrouping info)
        {
            return info.GroupedElements.Select<Element, Tag>((Func<Element, Tag>)(e => e.tag)).ToList<Tag>();
        }

        public static implicit operator Tag[](ElementGrouping info)
        {
            return info.GroupedElements.Select<Element, Tag>((Func<Element, Tag>)(e => e.tag)).ToArray<Tag>();
        }

        public static implicit operator string(ElementGrouping info)
        {
            return string.Join("&", info.GroupedElements.Select<Element, string>((Func<Element, string>)(t => t.tag.ToString())));
        }
    }
}
