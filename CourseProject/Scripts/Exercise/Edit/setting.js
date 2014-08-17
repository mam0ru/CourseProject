﻿(function() {
  $(function() {
    var $equations, $graphs, $images, $jqXHRData, $videos, createButton, createChildDiv, createFormulaElement, createInput, createParentDiv, getFormulas, getGraphs, getVideos, initFileUpload;
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
          var img, img1, img2, img3, input;
          alert("file uploaded");
          input = $("[name='Exercise.Text']")[0];
          img1 = "![]( ";
          img2 = data.result.path;
          img3 = " \"\")";
          img = img1 + img2 + img3;
          return input.value = input.value + img;
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
    $videos = [];
    getVideos = function() {
      var elements;
      elements = $('iframe');
      $.each(elements, function(e, value) {
        return $videos.push(value.src);
      });
      return $videos;
    };
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
    getGraphs = function() {
      var elements;
      elements = $("[name='GraphInfo']");
      $.each(elements, function(e, graph) {
        return $graphs.push(graph.value);
      });
      alert($graphs);
      return $graphs;
    };
    return $(document).ready(function() {
      $(".md-header").css('height', '35px');
      $(".jumbotron")[0].style.setProperty("height", "250px");
      $(document).on('click', '#addFormula', function(e) {
        var child, img, list;
        e.stopPropagation();
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
      $("#addVideo").on('click', function(e) {
        var parent;
        e.preventDefault();
        parent = createParentDiv();
        parent.innerHTML = $("#video").val();
        $("#listOfVideos").append(parent);
        return $("#video").val("");
      });
      $(document).on('click', "[name='delete']", function(e) {
        var parent1, parent2;
        e.stopPropagation();
        e.preventDefault();
        $(this).unbind();
        parent1 = $(this).parent();
        parent2 = parent1.parent();
        parent2.remove();
        return null;
      });
      initFileUpload();
      $(document).on('click', '#Upload', function(e) {
        e.stopPropagation();
        e.preventDefault();
        if ($jqXHRData) {
          alert("upload");
          $jqXHRData.submit();
        }
        return false;
      });
      $("#Submit").on('click', function(e) {
        var answers, equation, graphs, tags, videos;
        answers = $("#inputAnswers").textext()[0].hiddenInput().val();
        equation = getFormulas();
        equation = JSON.stringify(equation);
        tags = $("#inputTags").textext()[0].hiddenInput().val();
        graphs = getGraphs();
        graphs = JSON.stringify(graphs);
        videos = getVideos();
        videos = JSON.stringify(videos);
        $('input#Graphs').val(graphs);
        $('input#Answers')[0].value = answers;
        $('input#Tags')[0].value = tags;
        $('input#Equations').val(equation);
        $("input#Name")[0].value = $("#Exercise_Name").val();
        $("input#Text")[0].value = $("[name='Exercise.Text']").val();
        return $('input#Videos').val(videos);
      });
      return null;
    });
  });

}).call(this);

//# sourceMappingURL=setting.js.map
