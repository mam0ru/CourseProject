(function() {
  $(function() {
    var calculateExpression, points;
    points = [];
    calculateExpression = function(expr, f, t, s) {
      var point, variable, _results;
      variable = f;
      _results = [];
      while (variable < t + s / 2) {
        point = [
          variable, expr.evaluate({
            x: variable
          })
        ];
        points.push(point);
        _results.push(variable = variable + s);
      }
      return _results;
    };
    return $(document).ready(function() {
      return $("#addGraph").on('click', function(e) {
        var expression, formula, from, step, to;
        e.preventDefault();
        formula = $("#GraphFormula").val();
        from = parseFloat($("#RangeFrom").val());
        to = parseFloat($("#RangeTo").val());
        step = parseFloat($("#Step").val());
        expression = Parser.parse(formula);
        calculateExpression(expression, from, to, step);
        $.jqplot('chart3', [points], {
          series: [
            {
              showMarker: false
            }
          ],
          axes: {
            xaxis: {
              label: 'X'
            },
            yaxis: {
              label: 'Y'
            }
          }
        });
        return null;
      });
    });
  });

}).call(this);

//# sourceMappingURL=Graph.js.map
