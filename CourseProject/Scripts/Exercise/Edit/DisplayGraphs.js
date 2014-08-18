(function() {
  $(function() {
    var calculateExpression, draw, drawGraphs, points;
    points = [];
    drawGraphs = function(existing) {
      return $.each(existing, function(e, graph) {
        var expression, formula, from, graphDescription, parent, step, targetDiv, to;
        graphDescription = graph.value;
        graphDescription = JSON.parse(graphDescription);
        parent = graph.parentElement;
        targetDiv = parent.children[1];
        targetDiv.id = "currentDiv";
        formula = graphDescription[0];
        from = parseFloat(graphDescription[1]);
        to = parseFloat(graphDescription[2]);
        step = parseFloat(graphDescription[3]);
        expression = Parser.parse(formula);
        calculateExpression(expression, from, to, step);
        draw();
        points = [];
        return targetDiv.id = "";
      });
    };
    calculateExpression = function(expr, from, to, step) {
      var point, variable, _results;
      variable = from;
      _results = [];
      while (variable < to + step / 2) {
        point = [
          variable, expr.evaluate({
            x: variable
          })
        ];
        points.push(point);
        _results.push(variable = variable + step);
      }
      return _results;
    };
    draw = function() {
      return $.jqplot('currentDiv', [points], {
        series: [
          {
            showMarker: false
          }
        ],
        axes: {
          xaxis: {},
          yaxis: {}
        }
      });
    };
    return $(document).ready(function() {
      var existingGraphs;
      existingGraphs = $("[name='GraphInfo']");
      drawGraphs(existingGraphs);
      return $("#accordion").accordion({
        collapsible: true
      });
    });
  });

}).call(this);

//# sourceMappingURL=DisplayGraphs.js.map
