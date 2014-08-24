(function() {
  $(function() {
    var calculateExpression, createButton, createChildDiv, createContainerForGraph, createInfoElement, createParentDiv, points;
    points = [];
    createParentDiv = function() {
      var parent;
      parent = document.createElement('div');
      parent.className = 'row';
      return parent;
    };
    createChildDiv = function() {
      var childDiv;
      childDiv = document.createElement('div');
      childDiv.className = 'col-md-4';
      return childDiv;
    };
    createButton = function() {
      var button;
      button = document.createElement('button');
      button.name = "delete";
      button.className = "btn btn-danger";
      button.innerText = "Delete";
      return button;
    };
    createInfoElement = function(graph) {
      var info;
      info = document.createElement('input');
      info.type = 'hidden';
      info.name = 'GraphInfo';
      info.value = JSON.stringify(graph);
      return info;
    };
    createContainerForGraph = function(graph) {
      var GraphInfo, childButton, childDivForButton, childDivForGraph, parent;
      parent = createParentDiv();
      GraphInfo = createInfoElement(graph);
      childDivForGraph = createChildDiv();
      childDivForGraph.className = "col-md-4 thumbnail";
      childDivForGraph.id = "currentDiv";
      childDivForButton = createChildDiv();
      childButton = createButton("delete", "Удалить");
      childDivForButton.appendChild(childButton);
      parent.appendChild(GraphInfo);
      parent.appendChild(childDivForGraph);
      parent.appendChild(childDivForButton);
      return parent;
    };
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
        var container, expression, formula, from, graph, step, to;
        e.preventDefault();
        formula = $("#GraphFormula").val();
        from = parseFloat($("#RangeFrom").val());
        to = parseFloat($("#RangeTo").val());
        step = parseFloat($("#Step").val());
        if (step < (to - from) / 100) {
          step = (to - from) / 100;
        }
        expression = Parser.parse(formula);
        graph = [formula, from, to, step];
        container = createContainerForGraph(graph);
        $('#listOfGraphs')[0].appendChild(container);
        calculateExpression(expression, from, to, step);
        $.jqplot('currentDiv', [points], {
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
        $('#currentDiv')[0].id = "";
        points = [];
        return null;
      });
    });
  });

}).call(this);

//# sourceMappingURL=Graph.js.map
