(function() {
  $(function() {
    var $equations, $graphs, $images, $jqXHRData, createButton, createChildDiv, createFormulaElement, createImageElement, createInput, createParentDiv, getFormulas, getGraphs, getImages, initFileUpload;
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
    createInput = function(name) {
      var input;
      input = document.createElement('input');
      input.type = 'text';
      input.name = name;
      return input;
    };
    createButton = function(name, text) {
      var button;
      button = document.createElement('button');
      button.name = name;
      button.innerText = text;
      return button;
    };
    $jqXHRData = null;
    createImageElement = function(src) {
      var childButton, childDivForButton, childDivForImage, childImage, parent;
      parent = createParentDiv();
      childImage = document.createElement('img');
      childImage.src = src;
      childDivForImage = createChildDiv();
      childDivForImage.className = "col-md-4 thumbnail";
      childDivForButton = createChildDiv();
      childDivForImage.appendChild(childImage);
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForImage);
      parent.appendChild(childDivForButton);
      return parent;
    };
    createFormulaElement = function(formula) {
      var childButton, childDivForButton, childDivForImage, parent;
      parent = createParentDiv();
      childDivForImage = createChildDiv();
      childDivForImage.className = "col-md-4";
      childDivForButton = createChildDiv();
      childDivForImage.innerHTML = formula;
      childDivForImage.setAttribute('name', 'AddEquation');
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForImage);
      parent.appendChild(childDivForButton);
      return parent;
    };
    initFileUpload = function() {
      return $('#imageupload').fileupload({
        url: '/Exercise/UploadImage',
        dataType: 'json',
        add: function(e, data) {
          return $jqXHRData = data;
        },
        done: function(event, data) {
          var jsItem;
          alert("file uploaded");
          jsItem = createImageElement(data.result.path);
          return $('#listOfPictures').append(jsItem);
        },
        fail: function(event, data) {
          alert("ERROR");
          if (data.files[0].error) {
            return alert(data.files[0].error);
          }
        }
      });
    };
    $equations = [];
    $images = [];
    $graphs = [];
    getFormulas = function() {
      var elements;
      elements = $("[name='AddEquation']");
      $.each(elements, function(e, value) {
        var img;
        img = value.children[0];
        return $equations.push(img.src);
      });
      return $equations;
    };
    getImages = function() {
      var im;
      im = $("#listOfPictures > .row > .thumbnail > img");
      $.each(im, function(e, val) {
        return $images.push(val.src);
      });
      return $images;
    };
    getGraphs = function() {
      var elements;
      elements = $("[name='GraphInfo']");
      $.each(elements, function(e, graph) {
        return $graphs.push(graph.value);
      });
      return $graphs;
    };
    return $(document).ready(function() {
      $(document).on('click', '#addFormula', function(e) {
        var child, img, list;
        e.preventDefault();
        img = $('#equationToImg')[0];
        list = $("#listOfFormulas")[0];
        if (img.value) {
          child = createFormulaElement(img.value);
          $('#equationToImg')[0].value = "";
          $('#equation')[0].src = "";
          $('#equationInput')[0].value = "";
          list.appendChild(child);
        }
        return null;
      });
      $(document).on('click', "[name='delete']", function(e) {
        var parent1, parent2;
        e.preventDefault();
        $(this).unbind();
        parent1 = $(this).parent();
        parent2 = parent1.parent();
        parent2.remove();
        return null;
      });
      initFileUpload();
      $(document).on('click', '#Upload', function(e) {
        e.preventDefault();
        if ($jqXHRData) {
          alert("upload");
          $jqXHRData.submit();
        }
        return false;
      });
      $("#addVideo").on('click', function(e) {
        var parent;
        e.preventDefault();
        parent = createParentDiv();
        parent.innerHTML = $("#video").val();
        $("#listOfVideos").append(parent);
        return $("#video").val("");
      });
      $("#Submit").on('click', function(e) {
        var answers, equation, graphs, images, tags;
        answers = $("#inputAnswers").textext()[0].hiddenInput().val();
        images = getImages();
        tags = $("#inputTags").textext()[0].hiddenInput().val();
        equation = getFormulas();
        equation = JSON.stringify(equation);
        graphs = getGraphs();
        graphs = JSON.stringify(graphs);
        $('input#Category').val($("select#Category").val());
        $('input#Answers').val(answers);
        $('input#Graphs').val(graphs);
        $('input#Formulas').val(equation);
        return $('input#Tags').val(tags);
      });
      return null;
    });
  });

}).call(this);

//# sourceMappingURL=setting.js.map
