<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <xsl:template match="/">
    <html>
      <body style="background-color:#f4eed7;">
        <h2>HTML Canvas</h2>
        <xsl:apply-templates select="CanvasContainer/canvas" />
      </body>
    </html>
  </xsl:template>
  <xsl:template match="canvas">
    <svg viewBox="0 0 2000 2000" xmlns="http://www.w3.org/2000/svg">
      <xsl:for-each select="Figure">
        <xsl:choose>
          <xsl:when test="@xsi:type='Circle'">
            <circle cx="{_x}" cy="{_y}" r="{_r}" style="fill:{Style/FillColor};stroke:{Style/StrokeColor};stroke-width:{Style/StrokeWidth};fill-opacity:{Style/FillOpacity};stroke-opacity:{Style/StrokeOpacity}" />
          </xsl:when>
          <xsl:when test="@xsi:type='Line'">
            <line x1="{_x1}" y1="{_y1}" x2="{_x2}" y2="{_y2}"  style="fill:{Style/FillColor};stroke:{Style/StrokeColor};stroke-width:{Style/StrokeWidth};fill-opacity:{Style/FillOpacity};stroke-opacity:{Style/StrokeOpacity}" />
          </xsl:when>
          <xsl:when test="@xsi:type='Rectangle'">
            <rect x="{_x1}" y="{_y1}" width="{_x2}" height="{_y2}"  style="fill:{Style/FillColor};stroke:{Style/StrokeColor};stroke-width:{Style/StrokeWidth};fill-opacity:{Style/FillOpacity};stroke-opacity:{Style/StrokeOpacity}" />
          </xsl:when>
          <xsl:when test="@xsi:type='Point'">
            <circle cx="{_x}" cy="{_y}" r="2"  style="fill:{Style/FillColor};stroke:{Style/StrokeColor};stroke-width:{Style/StrokeWidth};fill-opacity:{Style/FillOpacity};stroke-opacity:{Style/StrokeOpacity}" />
          </xsl:when>
          <xsl:otherwise>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
    </svg>
  </xsl:template>
</xsl:stylesheet>