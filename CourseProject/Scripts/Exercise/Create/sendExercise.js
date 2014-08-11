(function() {
  var $answers, $equations, $images, $tags, getAnswers, getFormulas, getImages, getName, getTags, getText;

  $equations = [];

  $images = [];

  $tags = [];

  $answers = [];

  getFormulas = function() {
    var elements;
    elements = $("#formulas > #formula");
    $.each(elements, function(e, value) {
      return $equations.push(value.innerHTML);
    });
    return $equations;
  };

  getImages = function() {
    var im;
    im = $("#uploaded_images > img");
    $.each(im, function(e, val) {
      return $images.push(val.src);
    });
    return $images;
  };

  getTags = function() {
    var t;
    t = $("#Tags > .row > [name='tag']");
    $.each(t, function(e, val) {
      return $tags.push(val.value);
    });
    return $tags;
  };

  getAnswers = function() {
    var ans;
    ans = $("#answer > .row > [name='answer']");
    $.each(ans, function(e, val) {
      return $answers.push(val.value);
    });
    return $answers;
  };

  getName = function() {
    var $name;
    return $name = $('#Name').val();
  };

  getText = function() {
    var $text;
    return $text = $('#Text').val();
  };

  $(document).ready(function() {
    return $('#sendExercise').on('click', function(e) {
      var answers, category, formulas, images, name, tags, text;
      text = getText();
      name = getName();
      answers = getAnswers();
      formulas = getFormulas();
      images = getImages();
      tags = getTags();
      category = $("select#Category").val();
      $('input#Category').val(category);
      $('input#Answers').val(answers);
      $('input#Formulas').val(formulas);
      $('input#Pictures').val(images);
      return $('input#Tags').val(tags);
    });
  });

}).call(this);

//# sourceMappingURL=sendExercise.js.map
