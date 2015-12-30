﻿namespace AngleSharp.Core.Tests
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/domjs-unsafe.dat
    /// </summary>
    [TestFixture]
    public class UserJsUnsafeTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void Html5LibSvgCdata()
        {
            var doc = Html(@"<svg><![CDATA[foo
bar]]>");
            var html = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Length);
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var htmlhead = html.ChildNodes[0] as Element;
            Assert.AreEqual(0, htmlhead.ChildNodes.Length);
            Assert.AreEqual(0, htmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, htmlhead.NodeType);

            var htmlbody = html.ChildNodes[1] as Element;
            Assert.AreEqual(1, htmlbody.ChildNodes.Length);
            Assert.AreEqual(0, htmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, htmlbody.NodeType);

            var htmlbodysvg = htmlbody.ChildNodes[0] as Element;
            Assert.IsTrue(htmlbodysvg.Flags.HasFlag(NodeFlags.SvgMember));
            Assert.AreEqual(NamespaceNames.SvgUri, htmlbodysvg.NamespaceUri);
            Assert.AreEqual(1, htmlbodysvg.ChildNodes.Length);
            Assert.AreEqual(0, htmlbodysvg.Attributes.Length);
            Assert.AreEqual(NodeType.Element, htmlbodysvg.NodeType);

            var text = htmlbodysvg.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("foo\nbar", text.TextContent);
        }

        [Test]
        public void Html5LibScriptDataCommentStarted()
        {
            var doc = Html(@"<script type=""data""><!--foo" + Symbols.Null.ToString() + "</script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.GetNamedItem("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--foo" + Symbols.Replacement.ToString(), text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibScriptDataCommentFinishing()
        {
            var doc = Html(@"<script type=""data""><!-- foo--" + Symbols.Null.ToString() + "</script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.GetNamedItem("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo--" + Symbols.Replacement.ToString(), text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibScriptDataEnding()
        {
            var doc = Html(@"<script type=""data""><!-- foo-<</script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.GetNamedItem("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo-<", text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibScriptDataParagraph()
        {
            var doc = Html(@"<script type=""data""><!--<p></script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.GetNamedItem("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--<p>", text.TextContent);
                        
            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibDoctypeInHeadImplicit()
        {
            var doc = Html(@"<html><!DOCTYPE html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibDoctypeInBodyImplicit()
        {
            var doc = Html(@"<html><head></head><!DOCTYPE html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void HtmlParseForeignElementsWithInvalidAttribute()
        {
            var doc = Html(@"<!DOCTYPE html><html><body><svg alt=”Duke Engineering logo” version=""1.1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" x=""0px"" y=""0px"" viewBox=""0 0 300 47"" xml:space=""preserve""><path d=""M5.3 43.9c-1.3 0-1.9-0.8-1.9-2.2v-3h0.8v3c0 1 0.4 1.5 1.2 1.5 0.8 0 1.2-0.5 1.2-1.4v-3h0.8v3C7.2 43.1 6.5 43.9 5.3 43.9M16.8 43.8l-2-3.2c-0.1-0.2-0.3-0.5-0.4-0.6 0 0.2 0 0.9 0 1.2v2.7h-0.7v-5.1h0.8l2 3.1c0.1 0.2 0.4 0.6 0.4 0.7 0-0.2 0-0.9 0-1.2v-2.6h0.7v5.1H16.8""/><rect x=""24.1"" y=""38.6"" width=""0.8"" height=""5.1""/><path d=""M33.6 43.8h-0.8L31 38.6h0.8l1.1 3.3c0.1 0.3 0.2 0.8 0.3 0.9 0-0.2 0.2-0.6 0.3-0.9l1.1-3.3h0.8L33.6 43.8""/><polyline points=""41.5 43.8 41.5 38.6 44.8 38.6 44.8 39.4 42.3 39.4 42.3 40.7 43.8 40.7 43.8 41.4 42.3 41.4 42.3 43 45 43 45 43.8 41.5 43.8 ""/><path d=""M54 41.7l1 2.1h-0.9l-1-2h-1.2v2h-0.8v-5.1h2.3c0.9 0 1.7 0.5 1.7 1.5C55.1 41 54.7 41.5 54 41.7M53.4 39.4h-1.5V41h1.5c0.5 0 0.9-0.3 0.9-0.8C54.3 39.7 54 39.4 53.4 39.4zM62.9 43.9c-0.7 0-1.4-0.3-1.8-0.8l0.5-0.5c0.3 0.4 0.9 0.6 1.3 0.6 0.7 0 1.1-0.2 1.1-0.7 0-0.4-0.3-0.6-1.2-0.9 -1.1-0.3-1.6-0.6-1.6-1.5 0-0.9 0.7-1.4 1.7-1.4 0.7 0 1.2 0.3 1.7 0.7l-0.5 0.5c-0.3-0.3-0.7-0.5-1.2-0.5 -0.6 0-0.9 0.3-0.9 0.6 0 0.4 0.2 0.5 1.1 0.8 1 0.3 1.6 0.6 1.6 1.5C64.8 43.2 64.3 43.9 62.9 43.9""/><rect x=""71.2"" y=""38.6"" width=""0.8"" height=""5.1""/><polyline points=""80.4 39.4 80.4 43.8 79.6 43.8 79.6 39.4 78.1 39.4 78.1 38.6 81.9 38.6 81.9 39.4 80.4 39.4 ""/><path d=""M90.3 41.7v2h-0.8v-2l-1.8-3.1h0.9l0.8 1.4c0.2 0.3 0.4 0.8 0.5 1 0.1-0.2 0.3-0.7 0.5-1l0.8-1.4h0.9L90.3 41.7M5.9 34.9c-0.7 0-1.4 0-2.2 0.1C2.8 35 2.2 35 1.6 35c-0.1 0-0.3 0-0.5 0 -0.2 0-0.3-0.2-0.3-0.4 0-0.2 0.1-0.4 0.3-0.6 0.2-0.2 0.7-0.3 1.4-0.5 0.3 0 0.5-0.1 0.8-0.2 0.3-0.1 0.5-0.2 0.7-0.4 0.2-0.2 0.4-0.5 0.5-0.9 0.1-0.4 0.2-0.9 0.2-1.6V8.3c0-0.2-0.1-0.4-0.3-0.5C4.1 7.7 3.8 7.6 3.5 7.5c-0.3 0-0.7-0.1-1-0.1 -0.4 0-0.7 0-1-0.1C0.9 7.3 0.5 7.3 0.3 7.2 0.1 7.2 0 7 0 6.8c0-0.2 0.1-0.3 0.2-0.4 0.1-0.1 0.3-0.2 0.5-0.2C0.9 6 1.1 6 1.3 6c0.2 0 0.4 0 0.5 0h4.3c1.2 0 2.3 0 3.5-0.1 1.2-0.1 2.5-0.1 4-0.1 2.5 0 4.8 0.3 7 0.9 2.2 0.6 4.1 1.6 5.7 2.9 1.6 1.3 2.9 3 3.9 5.1 0.9 2.1 1.4 4.5 1.4 7.3 0 1.8-0.4 3.4-1.2 5 -0.8 1.6-1.8 2.9-3.1 4.1 -1.3 1.2-2.9 2.1-4.7 2.8 -1.8 0.7-3.7 1-5.7 1H5.9M8.7 29c0 0.8 0.2 1.5 0.5 2.1 0.3 0.6 0.8 1.1 1.4 1.5 0.6 0.4 1.2 0.7 1.9 0.9 0.7 0.2 1.5 0.3 2.2 0.3 0.6 0 1.4-0.1 2.2-0.3 0.9-0.2 1.7-0.5 2.7-0.9 0.9-0.4 1.8-0.9 2.7-1.6 0.9-0.7 1.7-1.5 2.5-2.4 0.7-0.9 1.3-2 1.8-3.3 0.4-1.2 0.7-2.7 0.7-4.3 0-1.6-0.2-3.1-0.7-4.4 -0.4-1.3-1-2.5-1.8-3.5 -0.8-1-1.6-1.9-2.6-2.7 -1-0.8-2-1.4-3.1-1.9 -1.1-0.5-2.2-0.9-3.3-1.1 -1.1-0.2-2.1-0.4-3.1-0.4 -1 0-1.7 0-2.3 0.1C9.8 7.3 9.4 7.5 9.2 7.6 8.9 7.7 8.8 7.9 8.7 8.1c0 0.2-0.1 0.4-0.1 0.5V29zM41.3 35.7c-1.1 0-2-0.2-2.8-0.5 -0.7-0.3-1.3-0.8-1.7-1.4 -0.4-0.6-0.7-1.3-0.9-2.1 -0.2-0.8-0.3-1.7-0.3-2.7 0-0.7 0-1.5 0-2.4 0-0.9 0-1.8 0-2.6 0-0.9 0-1.8 0-2.7 0-0.9 0-1.6 0-2.3 0-0.3-0.2-0.6-0.5-0.7 -0.3-0.1-0.6-0.2-0.9-0.2 -0.6-0.2-0.9-0.3-1.1-0.4 -0.2-0.1-0.3-0.2-0.3-0.4 0-0.2 0.1-0.3 0.3-0.4 0.2 0 0.5 0 0.8 0 0.3 0 0.7 0 1 0 0.3 0 0.7 0 1 0 0.3 0 0.7 0 1 0 0.3 0 0.7 0 1 0 0.3 0 0.5 0.1 0.6 0.2 0.1 0.1 0.1 0.4 0.1 0.7 0 2.1 0 4.2 0 6.3 0 2.1 0 4.2 0 6.3 0 1 0.3 1.7 1 2.2 0.7 0.5 1.6 0.8 2.7 0.8 1.3 0 2.3-0.1 3.1-0.4 0.7-0.3 1.3-0.6 1.7-1 0.4-0.4 0.6-0.8 0.7-1.3 0.1-0.5 0.2-1 0.2-1.5 0-1.7 0-3.4 0-5.1 0-1.7 0-3.3 0-5 0-0.1 0-0.2-0.1-0.3 0-0.1-0.2-0.2-0.3-0.2 -0.2-0.1-0.4-0.1-0.7-0.2 -0.3-0.1-0.8-0.2-1.3-0.4 -0.2-0.1-0.5-0.2-0.8-0.3 -0.4-0.1-0.5-0.3-0.5-0.5 0-0.2 0.1-0.3 0.3-0.3 0.2 0 0.5-0.1 0.7-0.1 0.4 0 0.8 0 1.2 0 0.4 0 0.8 0 1.2 0 0.5 0 0.9 0 1.4 0 0.5 0 0.9 0 1.4 0 0.2 0 0.4 0.1 0.6 0.2 0.3 0.1 0.4 0.4 0.4 0.8 0 2.4 0 4.8-0.1 7.2 -0.1 2.4-0.1 4.8-0.1 7.1 0 0.5 0.1 0.8 0.4 0.9 0.3 0.1 0.6 0.2 0.8 0.2 0.9 0 1.5 0 1.9 0 0.4 0 0.5 0.2 0.5 0.4 0 0.2-0.1 0.3-0.2 0.4 -0.1 0.1-0.4 0.1-0.7 0.2 -1.7 0.2-3 0.5-3.9 0.7 -1 0.2-1.5 0.3-1.8 0.3 -0.2 0-0.3-0.1-0.3-0.2 0-0.1 0-0.3 0-0.5v-1.1c0-0.4-0.1-0.6-0.3-0.6 -0.2 0-0.5 0.1-0.8 0.4 -0.3 0.3-0.8 0.6-1.3 1 -0.6 0.4-1.2 0.7-1.9 1C43 35.6 42.2 35.7 41.3 35.7M59.1 8c0-0.6-0.1-1.1-0.4-1.3 -0.2-0.3-0.5-0.5-0.8-0.6 -0.3-0.1-0.6-0.2-0.8-0.3s-0.4-0.2-0.4-0.4c0-0.2 0.1-0.3 0.2-0.4 0.2-0.1 0.4-0.2 0.8-0.3L61 3.5c0.1-0.1 0.3-0.1 0.4-0.1 0.1 0 0.3 0 0.4 0 0.1 0 0.2 0.1 0.4 0.2 0.1 0.1 0.2 0.3 0.2 0.7v19.4c0 0.5 0.1 0.8 0.4 0.8 0.1 0 0.3-0.1 0.7-0.3 0.4-0.2 0.9-0.5 1.5-0.8 0.6-0.3 1.2-0.7 1.8-1.1s1.2-0.8 1.8-1.2c0.5-0.4 1-0.7 1.3-1 0.4-0.3 0.5-0.5 0.5-0.7 0-0.3-0.1-0.6-0.3-0.7 -0.2-0.2-0.4-0.3-0.6-0.4 -0.2-0.1-0.4-0.2-0.6-0.3 -0.2-0.1-0.3-0.3-0.3-0.4 0-0.1 0.1-0.2 0.2-0.3 0.1-0.1 0.3-0.1 0.4-0.1 0.5 0 1.1 0 1.7 0 0.6 0 1.3 0 1.9 0 0.6 0 1.2 0 1.9 0 0.6 0 1.2 0 1.7 0 0.2 0 0.4 0 0.6 0.1 0.2 0.1 0.3 0.2 0.3 0.5 0 0.1-0.1 0.2-0.3 0.4 -0.2 0.1-0.4 0.2-0.6 0.3 -0.3 0.1-0.5 0.2-0.8 0.3 -0.3 0.1-0.5 0.1-0.6 0.2 -0.3 0.2-1 0.5-2 1.1 -1 0.5-2 1.1-3.1 1.8 -1.1 0.6-2 1.2-2.8 1.8 -0.8 0.6-1.2 0.9-1.2 1 0 0.2 0 0.3 0.1 0.4 0.1 0.1 0.2 0.2 0.2 0.3l8.2 7.4c0.5 0.4 0.9 0.8 1.4 1 0.5 0.2 0.9 0.4 1.2 0.5s0.6 0.3 0.8 0.4c0.2 0.1 0.3 0.2 0.3 0.4 0 0.3-0.4 0.5-1.2 0.5 -0.5 0-1.1 0-1.7-0.1 -0.6-0.1-1.3-0.1-2-0.1 -0.6 0-1.2 0-1.8 0.1C71 35 70.5 35 69.9 35c-0.5 0-0.9 0-1.1-0.1 -0.2 0-0.3-0.2-0.3-0.4 0-0.2 0.1-0.4 0.4-0.4 0.3-0.1 0.6-0.2 1-0.2 0.3 0 0.5-0.1 0.6-0.2 0.2-0.1 0.3-0.3 0.3-0.4 0-0.2-0.2-0.4-0.5-0.8l-7.1-6.7c-0.2-0.2-0.3-0.3-0.4-0.3 -0.2 0-0.3 0.1-0.3 0.2v6.7c0 0.5 0.1 0.9 0.4 1.1 0.2 0.2 0.5 0.3 0.7 0.4 0.4 0.1 0.7 0.2 1 0.3 0.3 0 0.4 0.2 0.4 0.4 0 0.2-0.1 0.3-0.4 0.4C64.2 35 63.9 35 63.7 35c-0.6 0-1.1 0-1.6-0.1 -0.5-0.1-1-0.1-1.6-0.1 -0.6 0-1.2 0-1.8 0.1C58 35 57.5 35 57.1 35c-0.2 0-0.4 0-0.7-0.1 -0.3 0-0.4-0.2-0.4-0.4 0-0.2 0.1-0.4 0.2-0.4 0.2-0.1 0.4-0.1 0.6-0.2 0.3-0.1 0.6-0.2 0.9-0.2 0.3-0.1 0.5-0.2 0.8-0.3 0.2-0.1 0.4-0.3 0.5-0.6 0.1-0.3 0.2-0.6 0.2-1.1V8M81.3 23.9c-0.1 0.2-0.1 0.4-0.1 0.7 0 0.3 0 0.6 0 1 0 1 0.1 2 0.3 2.9 0.2 0.9 0.5 1.8 1 2.5 0.5 0.7 1.2 1.3 2.1 1.8 0.9 0.5 2.1 0.7 3.5 0.7 0.9 0 1.6-0.1 2.1-0.3 0.5-0.2 0.9-0.4 1.2-0.6 0.3-0.2 0.5-0.5 0.7-0.7 0.2-0.2 0.3-0.3 0.5-0.3 0.3 0 0.4 0.1 0.4 0.4 0 0.1-0.1 0.3-0.4 0.7 -0.3 0.4-0.7 0.8-1.2 1.3 -0.6 0.5-1.3 0.9-2.1 1.2 -0.9 0.4-1.9 0.5-3.1 0.5 -2.6 0-4.6-0.9-6-2.6 -1.4-1.7-2.1-4.1-2.1-7 0-1.1 0.2-2.2 0.5-3.4 0.3-1.2 0.9-2.3 1.6-3.2 0.7-1 1.6-1.8 2.7-2.4 1.1-0.6 2.3-0.9 3.8-0.9 1.4 0 2.5 0.3 3.4 0.8 0.9 0.5 1.7 1.1 2.2 1.8 0.6 0.7 1 1.4 1.2 2.2 0.3 0.8 0.4 1.4 0.4 1.9 0 0.4-0.1 0.7-0.2 0.9 -0.1 0.1-0.4 0.2-0.8 0.2H81.3M85.9 22.5c1 0 1.9 0 2.5-0.1 0.6-0.1 1.1-0.2 1.5-0.4 0.3-0.2 0.6-0.4 0.7-0.7 0.1-0.3 0.2-0.6 0.2-1.1 0-0.8-0.3-1.5-1-2.1 -0.7-0.5-1.5-0.8-2.5-0.8 -0.8 0-1.6 0.2-2.3 0.5 -0.7 0.4-1.2 0.8-1.7 1.3 -0.5 0.5-0.8 1-1.1 1.6 -0.3 0.5-0.4 1-0.4 1.4 0 0.1 0 0.2 0.1 0.2 0 0 0.2 0.1 0.5 0.1 0.3 0 0.7 0.1 1.2 0.1S84.9 22.5 85.9 22.5z""/><rect x=""110.1"" width=""0.6"" height=""47.3""/><path d=""M128.2 41.1V25.6h10v2.3h-7.7v3.8h4.5V34h-4.5v4.9h8v2.3H128.2zM154.2 41.1l-6.2-9.7c-0.4-0.6-0.9-1.5-1.2-1.9 0 0.6 0 2.7 0 3.6v8.1h-2.3V25.6h2.4l5.9 9.5c0.4 0.6 1.1 1.8 1.3 2.2 0-0.6 0-2.8 0-3.7v-8h2.3v15.5H154.2zM169.1 41.4c-4 0-6.6-3.3-6.6-8s2.6-8 6.6-8c2.5 0 4.2 1 5.3 2.9l-2 1.3c-0.8-1.3-1.6-1.8-3.2-1.8 -2.7 0-4.2 2.2-4.2 5.7 0 3.5 1.5 5.7 4.2 5.7 2.5 0 3.5-1.7 3.5-3.5v-0.1h-2.5v-2.2h4.9V35C175 39 172.6 41.4 169.1 41.4zM181.2 41.1V25.6h2.3v15.5H181.2zM200.1 41.1l-6.2-9.7c-0.4-0.6-0.9-1.5-1.2-1.9 0 0.6 0 2.7 0 3.6v8.1h-2.3V25.6h2.4l5.9 9.5c0.4 0.6 1.1 1.8 1.3 2.2 0-0.6 0-2.8 0-3.7v-8h2.3v15.5H200.1zM209.1 41.1V25.6h10v2.3h-7.7v3.8h4.5V34h-4.5v4.9h8v2.3H209.1zM225.5 41.1V25.6h10v2.3h-7.7v3.8h4.5V34h-4.5v4.9h8v2.3H225.5zM250.4 34.8l3.2 6.4h-2.7l-3.1-6.2h-3.5v6.2H242V25.6h6.8c2.7 0 5 1.4 5 4.6C253.8 32.7 252.5 34.2 250.4 34.8zM248.8 27.8h-4.5v5h4.5c1.6 0 2.6-0.8 2.6-2.5C251.4 28.7 250.4 27.8 248.8 27.8zM260.3 41.1V25.6h2.3v15.5H260.3zM279.1 41.1l-6.2-9.7c-0.4-0.6-0.9-1.5-1.2-1.9 0 0.6 0 2.7 0 3.6v8.1h-2.3V25.6h2.4l5.9 9.5c0.4 0.6 1.1 1.8 1.3 2.2 0-0.6 0-2.8 0-3.7v-8h2.3v15.5H279.1zM294 41.4c-4 0-6.6-3.3-6.6-8s2.6-8 6.6-8c2.5 0 4.2 1 5.3 2.9l-2 1.3c-0.8-1.3-1.6-1.8-3.2-1.8 -2.7 0-4.2 2.2-4.2 5.7 0 3.5 1.5 5.7 4.2 5.7 2.5 0 3.5-1.7 3.5-3.5v-0.1H295v-2.2h4.9V35C299.9 39 297.5 41.4 294 41.4z""/><path d=""M133.2 14.1h-2.5v3.6h-1.4V8.1h4c1.8 0 3.1 1 3.1 3C136.4 13.1 135 14.1 133.2 14.1zM133.2 9.5h-2.6v3.3h2.6c1.1 0 1.7-0.6 1.7-1.7C134.9 10 134.2 9.5 133.2 9.5zM144.8 13.8l2 4h-1.7l-1.9-3.8H141v3.8h-1.4V8.1h4.2c1.7 0 3.1 0.9 3.1 2.9C147 12.5 146.2 13.5 144.8 13.8zM143.9 9.5H141v3.1h2.8c1 0 1.6-0.5 1.6-1.5C145.5 10 144.8 9.5 143.9 9.5zM156.8 17.8l-0.8-2.3h-3.9l-0.8 2.3h-1.5l3.5-9.7h1.5l3.5 9.7H156.8zM154.7 11.9c-0.2-0.7-0.6-1.8-0.7-2.2 -0.1 0.4-0.5 1.5-0.7 2.2l-0.8 2.2h3L154.7 11.9zM164 9.5v8.2h-1.5V9.5h-2.8V8.1h7.1v1.4H164zM173.8 9.5v8.2h-1.5V9.5h-2.8V8.1h7.1v1.4H173.8zM187.9 17.9c-1.4 0-2.6-0.6-3.4-1.6l1-1c0.6 0.7 1.6 1.1 2.5 1.1 1.4 0 2-0.5 2-1.4 0-0.7-0.6-1.1-2.2-1.6 -2-0.6-3-1.1-3-2.8 0-1.7 1.4-2.7 3.2-2.7 1.3 0 2.3 0.5 3.2 1.3l-1 1c-0.6-0.6-1.3-0.9-2.3-0.9 -1.1 0-1.6 0.6-1.6 1.2 0 0.7 0.4 1 2.1 1.5 1.9 0.6 3.1 1.2 3.1 2.9C191.5 16.7 190.4 17.9 187.9 17.9zM198.6 17.9c-2.5 0-4.1-2.1-4.1-5 0-2.9 1.6-5 4.1-5 1.6 0 2.6 0.6 3.3 1.8l-1.3 0.8c-0.5-0.8-1-1.2-2-1.2 -1.7 0-2.6 1.4-2.6 3.5 0 2.2 0.9 3.5 2.6 3.5 1 0 1.6-0.4 2.1-1.2l1.2 0.8C201.2 17.2 200 17.9 198.6 17.9zM211.3 17.8v-4.3h-4.4v4.3h-1.5V8.1h1.5V12h4.4V8.1h1.5v9.7H211.3zM220.3 17.9c-2.4 0-4.1-2-4.1-5 0-3 1.7-5 4.1-5 2.5 0 4.1 2 4.1 5C224.4 15.9 222.7 17.9 220.3 17.9zM220.2 9.4c-1.5 0-2.6 1.3-2.6 3.5s1.1 3.6 2.7 3.6c1.5 0 2.6-1.3 2.6-3.5C222.9 10.7 221.8 9.4 220.2 9.4zM231.5 17.9c-2.4 0-4.1-2-4.1-5 0-3 1.7-5 4.1-5 2.5 0 4.1 2 4.1 5C235.6 15.9 233.9 17.9 231.5 17.9zM231.4 9.4c-1.5 0-2.6 1.3-2.6 3.5s1.1 3.6 2.7 3.6c1.5 0 2.6-1.3 2.6-3.5C234.1 10.7 233 9.4 231.4 9.4zM239 17.8V8.1h1.4v8.2h4.8v1.4H239z""/><path d=""M255.6 12.2c1.4 0 1.7 1.2 1.7 2.3 0 1.8-1.6 3.5-3.1 3.5 -1.2 0-1.7-1.1-1.7-2C252.5 13.8 254.3 12.2 255.6 12.2zM254 17.6c1.1 0 2.4-3.7 2.4-4.3 0-0.5-0.2-0.7-0.6-0.7 -0.9 0-2.4 3.6-2.4 4.4C253.4 17.3 253.6 17.6 254 17.6zM263 12.4C263 12.4 263.1 12.4 263 12.4c0.1 0.3 0 0.5-0.1 0.5l-1.1 0c-0.2 0-0.2 0.1-0.3 0.3l-1 3c-0.4 1.4-1.1 4.6-3.2 4.6 -0.5 0-0.9-0.3-0.9-0.8 0-0.3 0.2-0.6 0.6-0.6 0.3 0 0.5 0.2 0.5 0.4 0 0.3 0.1 0.5 0.2 0.5 0.5 0 0.5-0.8 1-2.3 0.5-1.6 1-3.3 1.4-4.4 0.1-0.2 0.1-0.4 0.1-0.5 0-0.1-0.1-0.1-0.2-0.2l-0.9-0.1c-0.1 0-0.2-0.1-0.2-0.2 0-0.3 0.1-0.3 0.2-0.3h1c0.2 0 0.3-0.2 0.3-0.3 0.7-1.9 1.9-3.5 3.5-3.5 0.5 0 1 0.2 1 0.6 0 0.3-0.2 0.5-0.5 0.5 -0.2 0-0.4-0.1-0.6-0.3 -0.1-0.1-0.3-0.3-0.5-0.3 -0.1 0-0.2 0-0.3 0.1 -0.4 0.3-1.5 2.8-1.5 3.1 0 0 0 0.1 0.1 0.1H263z""/>
</svg></body></html>
");
            var svg = doc.QuerySelector("svg");
            Assert.AreEqual("", svg.GetAttribute("logo”"));
            Assert.AreEqual(10, svg.Attributes.Length);
        }

        [Test]
        public void HtmlParseSvgElementWithEmptyStyleShouldRemoveAttribute()
        {
            var doc = Html(@"<!DOCTYPE html><html><body><svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" version=""1.1"" focusable=""false"" baseProfile=""tiny"" id=""Layer_1"" x=""0px"" y=""0px"" viewBox=""0 0 8.5 4.9"" xml:space=""preserve"">
<polyline fill-rule=""evenodd"" fill=""#747474"" points=""8.5,0.6 7.9,0 4.3,3.6 0.6,0 0,0.6 4.3,4.9 "" style=""""/>
</svg></body></html>");
            var polyline = doc.QuerySelector("polyline");
            Assert.IsNull(polyline.GetAttribute("style"));
            Assert.AreEqual(3, polyline.Attributes.Length);
        }
    }
}
